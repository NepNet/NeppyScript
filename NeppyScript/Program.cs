using System;
using System.IO;
using NeppyScript.Lexer;

namespace NeppyScript
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var tokenizer = new Tokenizer("main", File.ReadAllText(args[0]));
			tokenizer.Generate();
			tokenizer.Clean();
			var tokens = tokenizer.Tokens;
			
			foreach (var token in tokens)
			{
				if (token.TokenType is TokenType.Whitespace or TokenType.Newline)
				{
					continue;
				}
				Console.WriteLine(token);
			}
		}
	}
}