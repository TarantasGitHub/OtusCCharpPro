namespace ClassLibrary.SpecialSymbols.Delimeters
{
    /// <summary>
    /// 
    /// </summary>
    public class UnknownDelimeter : Delimeter
    {
        public UnknownDelimeter()
        {

        }

        protected override char closingCharacter { get { return ' '; } }
    }
}
