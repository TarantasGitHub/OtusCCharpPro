using NumberGuessingLibrary;

namespace NumberGuessingLibraryTests;

public class ValueGeneratorTests
{
    [Theory]
    [MemberData(nameof(CorrectTestData))]
    public void CheckGeneratedValueTest(int from, int to, int? val)
    {
        // Act
        var value = ValueGenerator.GetValue(from, to);

        if(val.HasValue)
        {
            Assert.Equal(val.Value, value);
        }
        else
        {
            Assert.True(value >= from);
            Assert.True(value <= to);
        }
    }
    [Theory]
    [MemberData(nameof(WrongTestData))]
    public void CheckGeneratedValueWithWrongDataTest(int from, int to)
    {
        // Act
        Action act = () => ValueGenerator.GetValue(from, to);

        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Неправильно заданы аргументы", exception.Message);
    }

    public static IEnumerable<object[]> CorrectTestData
     => new List<object[]>
     {
        new object[] { 1, 1, 1 },
        new object[] { 0, 10, null },
        new object[] { 10, 100, null },
     };

     public static IEnumerable<object[]> WrongTestData
     => new List<object[]>
     {
        new object[] { 2, 1 },
        new object[] { 100, 10 },
        new object[] { 10, 9 },
     };
}
