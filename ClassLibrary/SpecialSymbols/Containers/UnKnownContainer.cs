namespace ClassLibrary.SpecialSymbols.Containers
{
    public class UnKnownContainer : BaseContainer
    {
        public void Parse(string text)
        {
            Parse(0, text.ToCharArray(), this);
        }

        protected long Parse(long index, char[] text, BaseContainer? parent = null)
        {
            Delimeter delimeter;
            for (; index < text.Length; index++)
            {
                delimeter = characterAnalyser(text[index]);
                if (delimeter != this)
                {
                    if (Childs == null)
                    {
                        Childs = new Stack<Delimeter>();
                    }
                    Childs.Push(delimeter);
                    delimeter.StartIndex = index;
                    index = delimeter.Parse(++index, text, this);
                }
                else if (closingCharacter == text[index])
                {
                    EndIndex = index;
                    return index;
                }
            }
            return index;
        }

        protected SpecialSymbol CharacterAnalyser(char symbol)
        {
            if (symbol.ToString() == this.ClosingSymbol.Symbol)
            {
                return this;
            }
            switch (symbol)
            {
                case '{':
                    return new Braces();
                case '[':
                    return new SquareBrackets();
                case '"':
                    return new DoubleQuotes();
                case ':':
                    return new Colon();
                case ',':
                    return new Comma();
                case '=':
                    return new Equals();
                default:
                    return this;
            }
        }
    }
}
