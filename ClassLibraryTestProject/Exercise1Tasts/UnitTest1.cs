using ClassLibrary.Exercise1;

namespace ClassLibraryTestProject.Exercise1Tasts
{
    public class UnitTest1
    {
        [Fact]
        public void CollectionIsEmptyTest()
        {
            // Arrange
            var collection = Array.Empty<object>();
            Func<object, float> converter = (object o) => 1.0F;

            // Act
            Action getMaxValue = () => collection.GetMax(converter);

            // Asserts
            var ex = Assert.Throws<ArgumentException>(getMaxValue);
            Assert.Equal("Коллекция пустая", ex.Message);
        }

        [Fact]
        public void ComparierIsEmptyTest()
        {
            // Arrange
            var collection = new List<object> { new object(), new object() };
            Func<object, float> converter = null;

            // Act
            Action getMaxValue = () => collection.GetMax(converter);

            // Asserts
            var ex = Assert.Throws<ArgumentException>(getMaxValue);
            Assert.Equal("Функция преобразования не задана", ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void SuccessTest(IEnumerable<TestObject> collection, Func<TestObject, float> convertToNumber, TestObject expectedResult)
        {
            // Act
            var getMaxValue = collection.GetMax(convertToNumber);

            // Asserts            
            Assert.Equivalent(expectedResult, getMaxValue);
        }
        public static IEnumerable<object[]> GetData()
        {
            var alex = new TestObject { Name = "Alex", Age = 80, Cach = 32000 };
            var fiona = new TestObject { Name = "Fiona", Age = 35, Cach = 4600000 };
            var angelina = new TestObject { Name = "Angelina", Age = 15, Cach = 456 };

            var allData = new List<object[]>
        {
            new object[] {
                new[] { alex, fiona, angelina},
                (TestObject to) => (float)to.Name.Length,
                angelina },
            new object[] {
                new[] { alex, fiona, angelina },
                (TestObject to) => (float)to.Age,
                alex },
            new object[] {  new[] { alex, fiona, angelina },
                (TestObject to) => (float)to.Cach,
                fiona }
        };

            return allData;
        }

        public class TestObject
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Cach { get; set; }
        }
    }
}