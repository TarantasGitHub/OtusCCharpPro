namespace ClassLibrary.SpecialSymbols.Delimeters
{
    public class Comma : Delimeter
    {
        protected override char closingCharacter { get { return ','; } }

        protected override long Parse(long index, char[] text, Delimeter? parent = null)
        {
            EndIndex = --index;
            return index;
        }
    }
}
