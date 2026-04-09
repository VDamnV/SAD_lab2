using System;
using GameSimulator.Events;
using GameSimulator.Models;

namespace GameSimulator.Interfaces;

public interface IGame
{
    string Title { get; }
    string Genre { get; }
    bool IsRunning { get; }
    
    event EventHandler<GameStateEventArgs> GameStateChanged;

    void Start(PC computer);
    void Stop();
    void Play();
}