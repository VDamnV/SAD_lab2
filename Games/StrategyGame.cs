using System;
using GameSimulator.Interfaces;
using GameSimulator.Models;

namespace GameSimulator.Games;

public class StrategyGame : GameBase, IInstallable
{
    public bool IsInstalled { get; private set; }
    public double RequiredSpaceMB { get; }

    // Конструктор приймає специфічні для гри дані та передає загальні в базовий клас
    public StrategyGame(string title, SystemRequirements minRequirements, double requiredSpaceMB) 
        : base(title, "Стратегія", minRequirements)
    {
        RequiredSpaceMB = requiredSpaceMB;
        IsInstalled = false; // За замовчуванням гра не інстальована
    }

    // Реалізація інтерфейсу IInstallable
    public void Install(double availableSpaceMB)
    {
        if (IsInstalled)
        {
            NotifyStateChanged($"Гра '{Title}' вже інстальована.");
            return;
        }

        // Перевірка умови з варіанту: "Гру не можна інсталювати, якщо недостатньо вільного місця на HDD"
        if (availableSpaceMB < RequiredSpaceMB)
        {
            NotifyStateChanged($"Помилка інсталяції '{Title}': недостатньо місця на HDD. Потрібно {RequiredSpaceMB} МБ, доступно {availableSpaceMB} МБ.");
            return;
        }

        IsInstalled = true;
        NotifyStateChanged($"Гру '{Title}' успішно інстальовано.");
    }

    // Реалізація абстрактного методу перевірки передумов перед запуском
    protected override bool CheckPrerequisites(PC computer)
    {
        // Перевірка умови: "Якщо гра не інстальована, не можна проводити будь-які маніпуляції з нею"
        if (!IsInstalled)
        {
            NotifyStateChanged($"Помилка запуску: гру '{Title}' спочатку потрібно інсталювати.");
            return false;
        }

        // Перевірка умови: "Гру не можна запустити, якщо апаратне забезпечення не відповідає мінімальним вимогам"
        if (!computer.MeetsRequirements(MinRequirements))
        {
         NotifyStateChanged($"Помилка запуску: ПК не відповідає мінімальним системним вимогам для '{Title}'.");
         return false;
        }

        // Примітка щодо умови "Ігри сумісні тільки з Windows PC":
        // Оскільки ми приймаємо об'єкт типу PC як параметр, ця умова вже виконується на рівні типізації.
        
        return true;
    }

    // Реалізація унікального ігрового процесу для стратегії
    protected override void ExecutePlayAction()
    {
        NotifyStateChanged($"[Ігровий процес] Ви граєте в '{Title}': Плетете інтриги, влаштовуєте династичні шлюби, фабрикуєте претензії на сусідні графства та розширюєте своє королівство!");
    }
}