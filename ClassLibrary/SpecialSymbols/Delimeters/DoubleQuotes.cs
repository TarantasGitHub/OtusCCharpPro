namespace ClassLibrary.SpecialSymbols.Delimeters
{
    public class DoubleQuotes : Delimeter
    {
        protected override char closingCharacter { get { return '"'; } }
    }
}