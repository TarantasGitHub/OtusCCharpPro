namespace NumberGuessingLibrary;

public class Game
{
    private event Action Attempting;
    private readonly AttemptCounter _counter;
    private readonly AttemptValueChecker _checker;
    private readonly int _from;
    private readonly int _to;

    public Game(int maxAttemptCount, int from, int to)
    {
        this._counter = new AttemptCounter(maxAttemptCount);
        this._checker = new AttemptValueChecker(ValueGenerator.GetValue(from, to));
        Attempting += this._counter.Increment;
        this._from = from;
        this._to = to;
    }

    public void Start()
    {
        Console.WriteLine(
            "Число загадано в диапазоне {0} - {1}, максимальное количество попыток: {2}",
            _from,
            _to,
            _counter.MaxAttemptCount);

        var stringUserValue = string.Empty;
        bool userWon = false;
        do
        {
            Console.WriteLine("Введите ваш вариант числа:");
            stringUserValue = Console.ReadLine();
            if (Int32.TryParse(stringUserValue, out int intUserValue))
            {
                Attempting();

                switch (_checker.Check(intUserValue))
                {
                    case AttemptStatus.Equal:
                        Console.WriteLine(
                            "Поздравляем, вы правильно определили загаданное число с {0} попыток.",
                            _counter.CurrentAttemptCount);
                        userWon = true;
                        break;
                    case AttemptStatus.Less:
                        Console.WriteLine("Ваше число меньше загаданного.");
                        break;
                    case AttemptStatus.More:
                        Console.WriteLine("Ваше число больше загаданного.");
                        break;
                    default:
                        throw new NotImplementedException("Такое поведение не было предусмотренно");
                }
            }

            if (_counter.GameOver())
            {
                Console.WriteLine("Вы проиграли. Все попытки угадать число потрачены.");
                break;
            }
        } while (!userWon);
    }
}
