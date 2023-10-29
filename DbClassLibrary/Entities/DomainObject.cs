namespace DbClassLibrary.Entities
{
    internal class DomainObject
    {
        private int id;
        private bool isDirty = false;
        public DomainObject(int id)
        {
            this.id = id;
        }

        public int GetId()
        {
            return this.id;
        }

        public void MarkDirty()
        {
            this.isDirty = true;
        }
    }
}
