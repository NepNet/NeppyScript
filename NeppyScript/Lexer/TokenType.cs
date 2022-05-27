namespace NeppyScript.Lexer
{
	public enum TokenType
	{
		None, //Some stuff that is just for placeholding?

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
		Power,
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