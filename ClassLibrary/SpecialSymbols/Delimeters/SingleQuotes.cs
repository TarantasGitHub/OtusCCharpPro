namespace ClassLibrary.SpecialSymbols.Delimeters
{
    /// <summary>
    /// Одинарная ковычка
    /// </summary>
    public class SingleQuotes : Delimeter
    {
        protected override char closingCharacter { get { return '\''; } }
    }
}