using System;
using GameSimulator.Events;

namespace GameSimulator.UI;

public static class ConsoleLogger
{
    // Метод для шаблону Observer (Події ігор)
    public static void LogGameStateChange(object? sender, GameStateEventArgs e)
    {
        Console.WriteLine($"[СИСТЕМА | {e.GameTitle}]: {e.Message}");
    }
    
    public static void LogMessage(string message)
    {
        Console.WriteLine($"[СИСТЕМА]: {message}");
    }

    public static void LogError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ПОМИЛКА]: {error}");
        Console.ResetColor();
    }
}