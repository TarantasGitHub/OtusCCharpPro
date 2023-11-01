namespace DbClassLibrary
{
    internal class PDOStatement
    {
        public void Execute<T>(T id) { }
        public Dictionary<string, object> Fetch() { return new Dictionary<string, object>(); }
        public void CloseCursor() { }
    }
}
