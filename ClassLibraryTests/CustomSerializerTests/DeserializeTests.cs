using ClassLibrary.Serializers;
using ClassLibraryTests.Models;

namespace ClassLibraryTests.CustomSerializerTests
{
    public class DeserializeTests
    {
        [Fact]
        public void DeserializeFromJsonTest1()
        {
            // Arrange
            var serializer = new CustomJsonSerializer();

            // Act
            var res = serializer.Deserialize<F>("{\"i1\":1,\"i2\":2,\"i3\":3,\"i4\":4,\"i5\":5}");
            
            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.i1);
            Assert.Equal(2, res.i2);
            Assert.Equal(3, res.i3);
            Assert.Equal(4, res.i4);
            Assert.Equal(5, res.i5);
        }

        [Fact]
        public void DeserializeFromCSVLineTest1()
        {
            // Arrange
            var serializer = new CustomJsonSerializer();

            // Act
            var res = serializer.Deserialize<F>("\"i1\"=1,\"i2\"=2,\"i3\"=3,\"i4\"=4,\"i5\"=5");

            // Assert
            Assert.NotNull(res);
            Assert.Equal(1, res.i1);
            Assert.Equal(2, res.i2);
            Assert.Equal(3, res.i3);
            Assert.Equal(4, res.i4);
            Assert.Equal(5, res.i5);
        }
    }
}
