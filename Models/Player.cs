using System.Collections.Generic;
using System.Linq;
using GameSimulator.Interfaces;
using GameSimulator.Games;
using GameSimulator.UI;

namespace GameSimulator.Models;

public class Player
{
    public string Name { get; }
    private readonly List<IGame> _ownedGames = new();

    public Player(string name)
    {
        Name = name;
    }

    public void AddGame(IGame game)
    {
        // Перевірка умови: тільки одна гра кожного жанру (крім симуляторів)
        if (game is not SimulatorGame && _ownedGames.Any(g => g.Genre == game.Genre))
        {
            ConsoleLogger.LogError($"У гравця {Name} вже є гра жанру '{game.Genre}'. Додати '{game.Title}' неможливо.");
            return;
        }

        _ownedGames.Add(game);
        ConsoleLogger.LogMessage($"Гравець {Name} додав до своєї бібліотеки гру '{game.Title}'.");
    }

    public void StartGame(IGame game, PC computer)
    {
        // Перевірка умови: грати одночасно можна тільки в одну гру
        if (_ownedGames.Any(g => g.IsRunning && g != game))
        {
            ConsoleLogger.LogError($"Помилка: Неможливо запустити '{game.Title}'. Спочатку закрийте поточну запущену гру.");
            return;
        }

        game.Start(computer);
    }
}