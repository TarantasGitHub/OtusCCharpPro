namespace ClassLibrary.Delimeters
{
    /// <summary>
    /// Точка с запятой
    /// </summary>
    public class Semicolon : Delimeter
    {
        protected override char closingCharacter { get { return ';'; } }

        protected override long Parse(long index, char[] text, Delimeter? parent = null)
        {
            this.EndIndex = --index;
            return index;
        }
    }
}