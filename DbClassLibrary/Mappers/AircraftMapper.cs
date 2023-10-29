using DbClassLibrary.Entities;

namespace DbClassLibrary.Mappers
{
    internal class AircraftMapper : Mapper
    {
        private PDOStatement selectStmt;
        private PDOStatement updadeStmt;
        private PDOStatement insertStmt;

        public AircraftMapper()
        {
            this.selectStmt = new PDOStatement();
            this.updadeStmt = new PDOStatement();
            this.insertStmt = new PDOStatement();
        }

        protected override DomainObject doCreateObject(Dictionary<string, object> raw)
        {
            throw new NotImplementedException();
        }

        protected override void doInsert(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        protected override PDOStatement SelectStmt()
        {
            throw new NotImplementedException();
        }

        protected string TargetClass()
        {
            return nameof(Aircraft);
        }
    }
}
