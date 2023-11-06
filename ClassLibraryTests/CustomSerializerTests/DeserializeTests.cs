using ClassLibrary;

namespace ClassLibraryTests.CustomSerializerTests
{
    public class DeserializeTests
    {
        [Fact]
        public void DeserializeTest1()
        {
            // Arrange
            var serializer = new CustomSerializer();

            // Act
            var res = serializer.Deserialize<F>("{\"i1\":1,\"i2\":2,\"i3\":3,\"i4\":4,\"i5\":5}");
            
            // Assert
            Assert.NotNull(res);
        }
    }

    public class F
        {
            public int i1, i2, i3, i4, i5;
            // public F Get() =>
            //     new F(){ i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 }; 
        }
}
