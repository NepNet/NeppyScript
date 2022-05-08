using System;
using System.IO;
using NeppyScript.Lexer;

namespace NeppyScript
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var tokens = new Tokenizer("main", File.ReadAllText(args[0])).Process();

			foreach (var token in tokens)
			{
				Console.WriteLine(token);
			}
		}
	}
}