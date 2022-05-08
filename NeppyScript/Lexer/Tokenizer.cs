using System;
using System.Collections.Generic;

namespace NeppyScript.Lexer
{
	public partial class Tokenizer : IStateMachine
	{
		private string _source;
		private string _file;

		public Tokenizer(string file, string source)
		{
			_source = source;
			_file = file;
		}

		public Token[] Process()
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
				else if (c is '+')
				{
					if (PeekNext(out char d) && d is '+')
					{
						MakeToken(_index, TokenType.Increment);
						_index++;
					}
					else
					{
						MakeToken(_index, TokenType.Addition);
					}
				}
				else if(c is '-')
				{
					if (PeekNext(out char d) && d is '-')
					{
						MakeToken(_index, TokenType.Decrement);
						_index++;
					}
					else
					{
						MakeToken(_index, TokenType.Subtraction);
					}
				}
				else if(c is '*')
				{
					if (PeekNext(out char d) && d is '*')
					{
						MakeToken(_index, TokenType.Power);
						_index++;
					}
					else
					{
						MakeToken(_index, TokenType.Multiplication);
					}
				}
				else if(c is '^')
				{
					MakeToken(_index, TokenType.Power);
				}
				else if(c is '%')
				{
					MakeToken(_index, TokenType.Modulo);
				}
				else if(c is '=')
				{
					if (PeekNext(out char d) && d is '=')
					{
						MakeToken(_index, TokenType.Equal);
						_index++;
					}
					else
					{
						MakeToken(_index, TokenType.Assignment);
					}
				}
				else if(c is ';')
				{
					MakeToken(_index, TokenType.StatementEnd);
				}
				else if (char.IsWhiteSpace(c))
				{
					if (c is '\n')
					{
						line++;
						lineStart = _index;
						MakeToken(_index - lineStart, TokenType.Newline);
					}
					else
					{
						MakeToken(_index - lineStart - 1, TokenType.Whitespace, c.ToString());
					}
				}
				else if (IsDigit(c))
				{
					/*
					if (c is '0')
					{
						if (ReadNext(out c))
						{
							switch (c)
							{
								case 'x' or 'X':
									//Parse as hex
									Console.WriteLine("HEX");
									break;
								case 'b' or 'B':
									//Parse as binary
									Console.WriteLine("BIN");
									break;
								case 'o' or 'O':
									//Parse as octal
									Console.WriteLine("OCT");
									break;
								default:
									Debug.ThrowError(this);
									break;
							}
						}
					}
					*/
					
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
			
			return tokens.ToArray();
		}
	}
}