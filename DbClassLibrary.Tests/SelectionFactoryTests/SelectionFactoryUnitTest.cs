using DbClassLibrary.IdentityObjects;
using DbClassLibrary.SelectionFactories;

namespace DbClassLibrary.Tests.SelectionFactoryTests
{
    public class SelectionFactoryUnitTest
    {
        [Fact]
        public void SelectionFactoryEmptyQyeryTest()
        {
            // Arange
            var aicraft = new AircraftIdentityObject();
            var selectionFactory = new AircraftSelectionFactory();

            // Act
            var query = selectionFactory.NewSelection(aicraft);

            //Assert
            Assert.Equal("SELECT \"AircraftCode\" ,\"Model\" ,\"Range\" FROM \"aircrafts\"", query.query);
        }

        [Fact]
        public void SelectionFactoryAircraftCodeTest()
        {
            // Arange
            var aicraft = new AircraftIdentityObject()
                .Field("AircraftCode")
                .eq("AircraftCodeTest");
            var selectionFactory = new AircraftSelectionFactory();

            // Act
            var query = selectionFactory.NewSelection(aicraft);

            //Assert
            Assert.Equal("SELECT \"AircraftCode\" ,\"Model\" ,\"Range\" FROM \"aircrafts\"\nWHERE \"AircraftCode\" = ?", query.query);
            Assert.Equal("AircraftCodeTest", query.values[0]);
        }

        [Fact]
        public void SelectionFactoryAircraftCodeSecondExpressionTest()
        {
            // Arange
            var aicraft = new AircraftIdentityObject()
                .Field("AircraftCode")
                .eq("AircraftCodeTest")
                .Field("AircraftCode")
                .eq("AircraftCodeTest2");
            var selectionFactory = new AircraftSelectionFactory();

            // Act
            var query = selectionFactory.NewSelection(aicraft);

            //Assert
            Assert.Equal("SELECT \"AircraftCode\" ,\"Model\" ,\"Range\" FROM \"aircrafts\"\nWHERE \"AircraftCode\" = ? AND \"AircraftCode\" = ?", query.query);
            Assert.Equal("AircraftCodeTest", query.values[0]);
            Assert.Equal("AircraftCodeTest2", query.values[1]);
        }

        [Fact]
        public void SelectionFactoryModelTest()
        {
            // Arange
            var aicraft = new AircraftIdentityObject()
                .Field("Model")
                .eq("ModelTest");
            var selectionFactory = new AircraftSelectionFactory();

            // Act
            var query = selectionFactory.NewSelection(aicraft);

            //Assert
            Assert.Equal("SELECT \"AircraftCode\" ,\"Model\" ,\"Range\" FROM \"aircrafts\"\nWHERE \"Model\" = ?", query.query);
            Assert.Equal("ModelTest", query.values[0]);
        }

        [Fact]
        public void SelectionFactoryRangeTest()
        {
            // Arange
            var aicraft = new AircraftIdentityObject()
                .Field("Range")
                .eq(5);
            var selectionFactory = new AircraftSelectionFactory();

            // Act
            var query = selectionFactory.NewSelection(aicraft);

            //Assert
            Assert.Equal("SELECT \"AircraftCode\" ,\"Model\" ,\"Range\" FROM \"aircrafts\"\nWHERE \"Range\" = ?", query.query);
            Assert.Equal(5, query.values[0]);
        }

        [Fact]
        public void SelectionFactoryAllFieldsTest()
        {
            // Arange
            var aicraft = new AircraftIdentityObject()
                .Field("AircraftCode")
                .eq("AircraftCodeTest")
                .Field("Model")
                .eq("ModelTest")
                .Field("Range")
                .eq(5);
            var selectionFactory = new AircraftSelectionFactory();

            // Act
            var query = selectionFactory.NewSelection(aicraft);

            //Assert
            Assert.Equal("SELECT \"AircraftCode\" ,\"Model\" ,\"Range\" FROM \"aircrafts\"\nWHERE \"AircraftCode\" = ? AND \"Model\" = ? AND \"Range\" = ?", query.query);
            Assert.Equal("AircraftCodeTest", query.values[0]);
            Assert.Equal("ModelTest", query.values[1]);
            Assert.Equal(5, query.values[2]);
        }
    }
}