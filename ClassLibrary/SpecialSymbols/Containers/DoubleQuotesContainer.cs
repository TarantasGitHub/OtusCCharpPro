namespace ClassLibrary.SpecialSymbols.Containers;

public class DoubleQuotesContainer : BaseContainer
{
    public DoubleQuotesContainer()
    {
        this.OpenningSymbol = new DoubleQuotes();
        this.ClosingSymbol = new DoubleQuotes();
    }
}
