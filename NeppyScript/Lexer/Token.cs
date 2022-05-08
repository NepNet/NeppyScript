namespace NeppyScript.Lexer
{
	public class Token
	{
		public string File { get; }
		public int Line { get; }
		public int Start { get; }
		public TokenType TokenType { get; }
		public string Value { get; }

		public Token(string file, int line, int start, TokenType tokenType, string value)
		{
			File = file;
			Line = line;
			Start = start;
			TokenType = tokenType;
			Value = value;
		}

		public override string ToString()
		{
			return $"['{File}', line {Line}, start {Start}] TokenType = '{TokenType}', Value = '{Value}'";
		}
	}
}