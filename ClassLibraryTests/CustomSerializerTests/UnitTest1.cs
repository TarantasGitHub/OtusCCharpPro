using ClassLibrary;

namespace ClassLibraryTests.CustomSerializerTests
{

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var f = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
            var serializer = new CustomSerializer();

            // Act
            var result = serializer.Serialize(f);

            // Assert
            Assert.Equal("{\"i1\":1,\"i2\":2,\"i3\":3,\"i4\":4,\"i5\":5}", result);
        }

        private class F
        {
            public int i1, i2, i3, i4, i5;
            // public F Get() =>
            //     new F(){ i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; 
        }
    }
}