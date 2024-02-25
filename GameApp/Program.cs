using System.Threading.Tasks;
using NumberGuessingLibrary;
using Microsoft.Extensions.Configuration;

namespace GameApp
{
    static class Program
    {
        static Task Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            Settings? settings = config.GetRequiredSection("GameSettings").Get<Settings>();

            var repository = new Repository(
                settings?.MaxAttemptCount ?? 10,
                settings?.From ?? 0,
                settings?.To ?? 100);
            var gameCreator = new GameCreator(repository);
            var game = gameCreator.GetGame();
            game.Start();
            return Task.CompletedTask;
        }
    }
}