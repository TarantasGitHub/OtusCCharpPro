using DbClassLibraryContracts.Models;

namespace DbClassLibrary.Mappers
{
    internal class AircraftMapper : Mapper<Aircraft, String>
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

        protected override Aircraft doCreateObject(Dictionary<string, object> raw)
        {
            throw new NotImplementedException();
        }

        protected override void doInsert(Aircraft obj)
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
