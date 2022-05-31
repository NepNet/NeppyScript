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
		OpenParenthesis,			// (
		CloseParenthesis,			// )
		OpenBracket,				// [
		CloseBracket,				// ]
		OpenCurlyBracket,			// {
		CloseCurlyBracket,			// }

		//Flow control
		If,
		Else,
		//Switch,
		
		//Loop
		While,
		For,
		Foreach,
		Break,

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
		Assignment,
		AssignAddition,
		AssignSubtraction,
		AssignMultiplication,
		AssignDivision,

		Increment,
		Decrement,
		
		//Logic stuff I guess
		LogicalOr,
		LogicalAnd,
		Negation,
		
		//Bit stuff???
		BitwiseOr,
		BitwiseAnd,
		ShiftLeft,
		ShiftRight,
		BitwiseInvert,
	}
}