using System.Collections.Generic;

namespace NeppyScript.Lexer
{
	public partial class Tokenizer
	{
		private const char ESCAPE = '\\';
		private bool IsDigit(char c) => char.IsDigit(c);
		private bool IsIdentifier(char c) => char.IsLetterOrDigit(c) || c is '_';
		
		private Dictionary<(char, char), TokenType> _shortTokens = new Dictionary<(char, char), TokenType>()
		{
			{('=','='), TokenType.Equal},
			{('=','\0'), TokenType.Assignment},
			{('+','+'), TokenType.Increment},
			{('+', '\0'), TokenType.Addition}
		};

		private bool IsShortToken(char c, out TokenType tokenType)
		{
			PeekNext(out char c2);
			if (_shortTokens.TryGetValue((c, c2), out tokenType))
			{
				_index++;
				return true;
			}
			else if(_shortTokens.TryGetValue((c, '\0'), out tokenType))
			{
				return true;
			}

			tokenType = TokenType.None;
			return false;
		}
		
		private Dictionary<string, TokenType> _keywords = new Dictionary<string, TokenType>()
		{
			
		};
	}
}