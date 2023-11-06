namespace ClassLibrary.Delimeters
{
    /// <summary>
    /// Базовый разделитель
    /// </summary>
    public abstract class Delimeter
    {
        public long StartIndex { get; set; }
        public long EndIndex { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }

        public Stack<Delimeter> Childs { get; set; }

        protected abstract char closingCharacter { get; }

        public void Parse(string text)
        {
            this.Parse(0, text.ToCharArray(), this);
        }

        protected virtual long Parse(long index, char[] text, Delimeter? parent = null)
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
                else if (this.closingCharacter == text[index])
                {
                    this.EndIndex = index;
                    return index;
                }
            }
            return index;
        }

        protected Delimeter characterAnalyser(char symbol)
        {
            if(symbol == this.closingCharacter)
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
                default:
                    return this;
            }
        }
    }
}