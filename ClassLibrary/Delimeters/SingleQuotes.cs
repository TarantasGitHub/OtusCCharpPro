namespace ClassLibrary.Delimeters
{
    /// <summary>
    /// Одинарная ковычка
    /// </summary>
    public class SingleQuotes : Delimeter
    {
        protected override char closingCharacter { get { return '\''; } }
    }
}