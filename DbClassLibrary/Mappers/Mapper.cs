using DbClassLibrary.Entities;

namespace DbClassLibrary.Mappers
{
    internal abstract class Mapper
    {
        
        public DomainObject Find(int id)
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

        public DomainObject CreateObject(Dictionary<string, object> raw)
        {
            var obj = this.doCreateObject(raw);
            return obj;
        }

        public void Insert(DomainObject obj)
        {
            this.doInsert(obj);
        }

        protected abstract DomainObject doCreateObject(Dictionary<string, object> raw);
        protected abstract void doInsert(DomainObject obj);
        protected abstract PDOStatement SelectStmt();
    }
}
