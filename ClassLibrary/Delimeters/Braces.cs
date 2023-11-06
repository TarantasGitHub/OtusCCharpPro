namespace ClassLibrary.Delimeters
{
    /// <summary>
    /// Фигурные скобки
    /// </summary>
    public class Braces : Delimeter
    {
        protected override char closingCharacter { get { return '}';}}
    }
}
