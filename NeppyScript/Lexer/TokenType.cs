namespace NeppyScript.Lexer
{
	public enum TokenType
	{
		Newline,
		Whitespace,
		Comment,

		StatementEnd,
		Separator,
		Identifier,		

		IntegerLiteral,
		FloatLiteral,
		StringLiteral,
		CharacterLiteral,

		//Grouping stuff i guess
		Parenthesis,		// ()
		Bracket,			// []
		Brace,				// {}

		//Flow control
		If,
		Else,
		Switch,

		//Math stuff
		Addition,
		Subtraction,
		Multiplication,
		Division,
		Modulo,
		Greater,
		Smaller,
		GreaterOrEqual,
		SmallerOrEqual,
		Equal,
		Negation,
		Assignment,

		Increment,
		Decrement
	}
}