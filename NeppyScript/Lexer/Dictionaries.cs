namespace NeppyScript.Lexer
{
	public partial class Tokenizer
	{
		private const char ESCAPE = '\\';
		private bool IsDigit(char c) => char.IsDigit(c);
		private bool IsIdentifier(char c) => char.IsLetterOrDigit(c) || c is '_';
	}
}