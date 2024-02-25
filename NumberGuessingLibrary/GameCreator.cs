namespace NumberGuessingLibrary;
public class GameCreator
{
    private readonly IRepository _repository;
    public GameCreator(IRepository repository)
    {
        this._repository = repository;
    }

    public Game GetGame()
    {
        return new Game(
            _repository.MaxAttemptCount(),
            _repository.From(),
            _repository.To());
    }
}