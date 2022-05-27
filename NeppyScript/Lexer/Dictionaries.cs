using System;
using System.Collections.Generic;

namespace NeppyScript.Lexer
{
	public partial class Tokenizer
	{
		private const char ESCAPE = '\\';
		private bool IsDigit(char c) => char.IsDigit(c);
		private bool IsIdentifier(char c) => char.IsLetterOrDigit(c) || c is '_';

		
#if DICTIONARYSHORTTOKENS
		private Dictionary<string, TokenType> _shortTokens = new Dictionary<string, TokenType>()
#else	
		private SortedList<string, TokenType> _shortTokens = new SortedList<string, TokenType>()
#endif
		{
			//MATH
			{"==", TokenType.Equal},
			{"=", TokenType.Assignment},
			{"++", TokenType.Increment},
			{"--", TokenType.Decrement},
			{"+", TokenType.Addition},
			{"-", TokenType.Subtraction},
			{"*", TokenType.Multiplication},
			{"/", TokenType.Division},
			{"^", TokenType.Power},
			{"**", TokenType.Power},
			{"%", TokenType.Modulo},
			{">", TokenType.Greater},
			{">=", TokenType.GreaterOrEqual},
			{"<", TokenType.Smaller},
			{"<=", TokenType.SmallerOrEqual},
			{"+=", TokenType.AssignAddition},
			{"-=", TokenType.AssignSubtraction},
			{"*=", TokenType.AssignMultiplication},
			{"/=", TokenType.AssignDivision},
			
			//BRACES AND STUFF
			{"(", TokenType.OpenParenthesis},
			{")", TokenType.CloseParenthesis},
			{"[", TokenType.OpenBracket},
			{"]", TokenType.CloseBracket},
			{"{", TokenType.OpenBrace},
			{"}", TokenType.CloseBrace},
			
			//Idk
			{",", TokenType.Separator},
			{";", TokenType.StatementEnd},
			{"!", TokenType.Negation},
			{"||", TokenType.LogicalOr},
			{"&&", TokenType.LogicalAnd},
			
			//Bits
			{"|", TokenType.BitwiseOr},
			{"&", TokenType.BitwiseAnd},
			{"<<", TokenType.ShiftLeft},
			{">>", TokenType.ShiftRight},
			{"~", TokenType.BitwiseInvert}
			
		};	
		
		private bool IsShortToken(char c, out TokenType tokenType)
		{
#if DICTIONARYSHORTTOKENS
			PeekNext(out char c2);
			if (_shortTokens.TryGetValue($"{c}{c2}", out tokenType))
			{
				_index++;
				return true;
			}
			if(_shortTokens.TryGetValue(c.ToString(), out tokenType))
			{
				return true;
			}
#else
			for (int i = 0; i < _shortTokens.Count; i++)
			{
				if (_shortTokens.Keys[i][0] != c) continue;

				tokenType = _shortTokens.Values[i];
				for (int j = i; j < _shortTokens.Count; j++)
				{
					if (_shortTokens.Keys[j][0] != c) continue;

					var key = _shortTokens.Keys[j];
					if (key.Length <= 1 || !PeekNext(out char c2)) continue;
					if (key[1] != c2) continue;

					_index++;
					tokenType = _shortTokens.Values[j];
					return true;
				}

				return true;
			}
#endif
			
			tokenType = TokenType.None;
			return false;
		}
		
		private Dictionary<string, TokenType> _keywords = new Dictionary<string, TokenType>()
		{
			
		};
	}
}