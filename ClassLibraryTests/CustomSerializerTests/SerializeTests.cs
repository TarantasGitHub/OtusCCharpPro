using ClassLibrary.Serializers;
using ClassLibraryTests.Models;

namespace ClassLibraryTests.CustomSerializerTests
{
    public class SerializeTests
    {
        [Fact]
        public void SerializeTest()
        {
            // Arrange
            var f = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
            var serializer = new CustomJsonSerializer();

            // Act
            var result = serializer.Serialize(f);

            // Assert
            Assert.Equal("{\"i1\":1,\"i2\":2,\"i3\":3,\"i4\":4,\"i5\":5}", result);
        }

    }
}
