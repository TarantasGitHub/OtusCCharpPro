namespace ClassLibrary.SpecialSymbols.Delimeters
{
    /// <summary>
    /// Двоеточие
    /// </summary>
    public class Colon : Delimeter
    {
        public Colon()
        {
            this.Symbol = ':';
        }
        protected override char closingCharacter { get { return ':'; } }

        protected override long Parse(long index, char[] text, Delimeter? parent = null)
        {
            EndIndex = --index;
            return index;
        }
    }
}