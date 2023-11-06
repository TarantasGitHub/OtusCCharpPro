namespace ClassLibrary.Delimeters
{
    /// <summary>
    /// Двоеточие
    /// </summary>
    public class Colon : Delimeter
    {
        protected override char closingCharacter { get { return ':'; } }

        protected override long Parse(long index, char[] text, Delimeter? parent = null)
        {
            this.EndIndex = --index;
            return index;
        }        
    }
}