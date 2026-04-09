using System;
using GameSimulator.Interfaces;
using GameSimulator.Games;
using GameSimulator.Models;

namespace GameSimulator.Factories;

public enum GameType { Strategy, Simulator, Casino }

// Шаблон Simple Factory (або параметризований Factory Method)
public static class GameFactory
{
    public static IGame CreateGame(GameType type, string title, SystemRequirements reqs, double requiredSpaceMB = 0)
    {
        return type switch
        {
            GameType.Strategy => new StrategyGame(title, reqs, requiredSpaceMB),
            GameType.Simulator => new SimulatorGame(title, reqs, requiredSpaceMB),
            GameType.Casino => new OnlineCasino(title, reqs),
            _ => throw new ArgumentException("Невідомий тип гри")
        };
    }
}