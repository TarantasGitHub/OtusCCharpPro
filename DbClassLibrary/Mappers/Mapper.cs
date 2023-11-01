using DbClassLibraryContracts.DomainObjects;

namespace DbClassLibrary.Mappers
{
    internal abstract class Mapper<TSource, TKey> where TSource : DomainObject<TKey>
    {
        
        public TSource Find(TKey id)
        {
            this.SelectStmt().Execute(id);
            var row = this.SelectStmt().Fetch();

            this.SelectStmt().CloseCursor();

            if(row == null)// if(! isarray($raw))
            {
                return null;
            }

            if (!row.ContainsKey("id"))
            {
                return null;
            }

            var obj = this.CreateObject(row);
            return obj;
        }

        public TSource CreateObject(Dictionary<string, object> raw)
        {
            var obj = this.doCreateObject(raw);
            return obj;
        }

        public void Insert(TSource obj)
        {
            this.doInsert(obj);
        }

        protected abstract TSource doCreateObject(Dictionary<string, object> raw);
        protected abstract void doInsert(TSource obj);
        protected abstract PDOStatement SelectStmt();
    }
}
