namespace ClassLibrary.SpecialSymbols.Containers
{
    public class BracesContainer : BaseContainer
    {
        public BracesContainer()
        {
            this.OpenningSymbol = new OpeningCurlyBrace();
            this.ClosingSymbol = new ClosingCurlyBrace();
        }
    }
}
