namespace NeppyScript.Lexer
{
	public partial class Tokenizer
	{
		private int _index;
		private bool ReadNext(out char c)
		{
			if (_index < _source.Length)
			{
				c = _source[_index++];
				return true;
			}

			c = '\0';
			return false;
		}

		private bool PeekNext(out char c)
		{
			if (_index < _source.Length)
			{
				c = _source[_index];
				return true;
			}
			c = '\0';
			return false;
		}
	}
}