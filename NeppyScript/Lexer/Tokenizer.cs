using System;
using System.Collections.Generic;

namespace NeppyScript.Lexer
{
	public partial class Tokenizer : IStateMachine
	{
		private string _source;
		private string _file;

		public Token[] Tokens { get; private set; }
		public int Length => Tokens.Length;
		public Token this[int i] => Tokens[i];

		public Tokenizer(string file, string source)
		{
			_source = source;
			_file = file;
			Tokens = new Token[0];
		}

		public void Generate()
		{
			_index = 0;
			int line = 0;
			int lineStart = 0;

			var tokens = new List<Token>();

			void MakeToken(int start, TokenType type, string value = "")
			{
				var token = new Token(_file, line, start, type, value);
				tokens.Add(token);
			}
			while (ReadNext(out char c))
			{
				if (c is '/')
				{
					//Process comment if so
					if (ReadNext(out c) && c is '/')
					{
						int start = _index;
						while (ReadNext(out c) && c is not '\n') { }
						_index--;
						
						int end = _index;

						string value = _source.Substring(start, end - start);
						
						MakeToken(start, TokenType.Comment, value);
					}
					//Process it as a division token
					else
					{
						_index--;
						MakeToken(_index - 1, TokenType.Division);
					}
				}
				else if(IsShortToken(c, out TokenType tokenType))
				{
					MakeToken(_index - 1, tokenType);
				}
				else if (char.IsWhiteSpace(c))
				{
					if (c is '\n')
					{
						line++;
						lineStart = _index;
#if NEWLINE_TOKENS
		
						MakeToken(_index - lineStart, TokenType.Newline);
#endif
					}
					else
					{
#if WHITESPACE_TOKENS
						MakeToken(_index - lineStart - 1, TokenType.Whitespace, c.ToString());
#endif
					}
				}
				else if (c is '\'')
				{
					//Character literal	
				}
				else if (IsDigit(c))
				{
					//Parse decimal
					int start = _index - 1;
					while (ReadNext(out c))
					{
						if (!IsDigit(c))
						{
							if (IsIdentifier(c))
							{
								Debug.ThrowError(this);
							}
							_index--;
							break;
						}
					}
					int end = _index;

					string value = _source.Substring(start, end - start);
					
					tokens.Add(new Token(_file, line, start - lineStart, TokenType.IntegerLiteral, value));
				}
				else if (IsIdentifier(c))
				{
					int start = _index - 1;
					while (ReadNext(out c) && IsIdentifier(c)) { }
					_index--;
					
					int end = _index;

					string value = _source.Substring(start, end - start);
					
					tokens.Add(new Token(_file, line, start - lineStart, TokenType.Identifier, value));
				}
				else
				{
					Debug.ThrowError(this);
				}
			}

			Tokens = tokens.ToArray();
		}

		public void Clean()
		{
			foreach (var token in Tokens)
			{
				if (token.TokenType is not TokenType.Identifier) continue;
				
				if (_keywords.TryGetValue(token.Value, out var newType))
				{
					token.TokenType = newType;
				}
			}
		}
	}
}