using System;

namespace GameSimulator.Events;

// Клас аргументів події
public class GameStateEventArgs : EventArgs
{
    public string Message { get; }
    public string GameTitle { get; }

    public GameStateEventArgs(string gameTitle, string message)
    {
        GameTitle = gameTitle;
        Message = message;
    }
}