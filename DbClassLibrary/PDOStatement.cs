namespace DbClassLibrary
{
    internal class PDOStatement
    {
        public void Execute(int id) { }
        public Dictionary<string, object> Fetch() { return new Dictionary<string, object>(); }
        public void CloseCursor() { }
    }
}
