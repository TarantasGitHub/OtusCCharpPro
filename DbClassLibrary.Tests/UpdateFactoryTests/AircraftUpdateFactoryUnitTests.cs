using DbClassLibrary.UpdateFactories;
using DbClassLibraryContracts.Models;

namespace DbClassLibrary.Tests.UpdateFactoryTests
{
    public class AircraftUpdateFactoryUnitTests
    {
        [Fact]
        public void UpdateFactoryEmptyQyeryTest()
        {
            // Arange
            var aircraft = new Aircraft();
            var updateFactory = new AircraftUpdateFactory();

            // Act
            var query = updateFactory.NewUpdate<Aircraft, String>(aircraft);

            //Assert
            Assert.Equal("INSERT INTO \"aircrafts\" (\r\n\"AircraftCode\", \"Model\", \"Range\"\r\n) VALUES (?, ?, ?)\r\n", query.query);
            Assert.Collection(query.values,
                value => { Assert.Equal(string.Empty, value); },
                value => { Assert.Equal(string.Empty, value); },
                value => { Assert.Equal(0, value); }
                );
        }

        [Fact]
        public void UpdateFactoryInsertAircraftCodeTest()
        {
            // Arange
            var aircraft = new Aircraft("aircraftCode", "model", 2);
            aircraft.MarkNew();
            var updateFactory = new AircraftUpdateFactory();

            // Act
            var query = updateFactory.NewUpdate<Aircraft, String>(aircraft);

            //Assert
            Assert.Equal("INSERT INTO \"aircrafts\" (\r\n\"AircraftCode\", \"Model\", \"Range\"\r\n) VALUES (?, ?, ?)\r\n", query.query);
            Assert.Collection(query.values,
                value => { Assert.Equal("aircraftCode", value); },
                value => { Assert.Equal("model", value); },
                value => { Assert.Equal(2, value); }
                );
        }


        [Fact]
        public void UpdateFactoryUpdateAircraftCodeTest()
        {
            // Arange
            var aircraft = new Aircraft("aircraftCode", "model", 2);

            var updateFactory = new AircraftUpdateFactory();

            // Act
            var query = updateFactory.NewUpdate<Aircraft, String>(aircraft);

            //Assert
            Assert.Equal("UPDATE \"aircrafts\" SET \r\n\"AircraftCode\" = ?, \"Model\" = ?, \"Range\" = ?\r\nWHERE \"AircraftCode\" = ?\r\n", query.query);
            Assert.Collection(query.values,
                value => { Assert.Equal("aircraftCode", value); },
                value => { Assert.Equal("model", value); },
                value => { Assert.Equal(2, value); },
                value => { Assert.Equal("aircraftCode", value); }
                );
        }
    }
}
