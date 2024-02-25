using NumberGuessingLibrary;
using Moq;

namespace NumberGuessingLibraryTests;

public class CreateGameTest
{
    
    [Fact]
    public void CreatingCreateGameTest()
    {
        // Arrange
        var repository = new Mock<IRepository>();
        
        // Act
        var gameCreator = new GameCreator(repository.Object);
        
        // Assert
        Assert.NotNull(gameCreator);
    }

    [Fact]
    public void CreatingGameTest()
    {
        // Arrange
        var repository = new Mock<IRepository>();
        var gameCreator = new GameCreator(repository.Object);

        // Act
        var game = gameCreator.GetGame();
        
        // Assert
        Assert.NotNull(game);
    }
}