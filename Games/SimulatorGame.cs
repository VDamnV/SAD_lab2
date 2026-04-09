using System;
using GameSimulator.Interfaces;
using GameSimulator.Models;

namespace GameSimulator.Games;

public class SimulatorGame : GameBase, IInstallable
{
    public bool IsInstalled { get; private set; }
    public double RequiredSpaceMB { get; }
    
    // Агрегація: симулятор може мати підключене кермо, але воно не обов'язкове (тому nullable '?' або перевірка на null)
    public SteeringWheel? AttachedWheel { get; private set; }

    public SimulatorGame(string title, SystemRequirements minRequirements, double requiredSpaceMB) 
        : base(title, "Симулятор", minRequirements)
    {
        RequiredSpaceMB = requiredSpaceMB;
        IsInstalled = false;
    }

    // Реалізація інтерфейсу IInstallable (ідентична до стратегії)
    public void Install(double availableSpaceMB)
    {
        if (IsInstalled)
        {
            NotifyStateChanged($"Гра '{Title}' вже інстальована.");
            return;
        }

        if (availableSpaceMB < RequiredSpaceMB)
        {
            NotifyStateChanged($"Помилка інсталяції '{Title}': недостатньо місця на HDD. Потрібно {RequiredSpaceMB} МБ.");
            return;
        }

        IsInstalled = true;
        NotifyStateChanged($"Гру '{Title}' успішно інстальовано.");
    }

    // Специфічний метод тільки для симуляторів
    public void ConnectSteeringWheel(SteeringWheel wheel)
    {
        AttachedWheel = wheel;
        NotifyStateChanged($"До гри '{Title}' підключено кермо: {wheel.ModelName}. Якість гри покращено!");
    }

    public void DisconnectSteeringWheel()
    {
        if (AttachedWheel != null)
        {
            NotifyStateChanged($"Кермо {AttachedWheel.ModelName} відключено від гри '{Title}'.");
            AttachedWheel = null;
        }
    }

    protected override bool CheckPrerequisites(PC computer)
    {
        if (!IsInstalled)
        {
            NotifyStateChanged($"Помилка запуску: гру '{Title}' спочатку потрібно інсталювати.");
            return false;
        }

        if (!computer.MeetsRequirements(MinRequirements))
        {
         NotifyStateChanged($"Помилка запуску: ПК не відповідає мінімальним системним вимогам для '{Title}'.");
         return false;
        }

        return true;
    }

    // Унікальний ігровий процес симулятора з перевіркою керма
    protected override void ExecutePlayAction()
    {
        // Перевіряємо наявність керма та формуємо відповідне повідомлення
        string qualityMessage = AttachedWheel != null 
            ? $"з максимальною реалістичністю завдяки керму {AttachedWheel.ModelName}" 
            : "на стандартній клавіатурі. Якість гри звичайна";

        NotifyStateChanged($"[Ігровий процес] Ви граєте в '{Title}': Проходите апекс на трасі Нюрбургринг {qualityMessage}.");
    }
}