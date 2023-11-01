namespace DbClassLibraryContracts.DomainObjects
{
    public abstract class DomainObject<TKey>
    {
        private TKey id;
        private string keyName;
        private bool isDirty = false;
        private bool isNew = false;

        public bool IsNew { get { return this.isNew; } }
        public DomainObject(TKey? id, string keyName = "id")
        {
            if (string.IsNullOrEmpty(keyName))
            {
                throw new ArgumentNullException("Название поля с идентификатором не может быть пустым");
            }

            this.keyName = keyName;

            if (id == null)
            {
                this.MarkNew();
            }
            else
            {
                this.id = id;
            }
        }

        public abstract Dictionary<string, object> GetPropertiesWithVAlues();

        public TKey GetId()
        {
            return id;
        }

        public string GetKeyName()
        {
            return keyName;
        }

        public void SetId(TKey id)
        {
            this.id = id; ;
        }

        public void MarkDirty()
        {
            isDirty = true;
        }

        public void MarkNew() {
            this.isNew = true;
        }
    }
}