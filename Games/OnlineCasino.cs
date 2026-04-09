using System;
using GameSimulator.Interfaces;
using GameSimulator.Models;

namespace GameSimulator.Games;

public class OnlineCasino : GameBase
{
    // Зверніть увагу: ми НЕ реалізуємо IInstallable!
    // Немає властивостей IsInstalled та RequiredSpaceMB.

    public OnlineCasino(string title, SystemRequirements minRequirements) 
        : base(title, "Online Casino", minRequirements)
    {
    }

    // Перевизначаємо метод перевірки умов перед запуском
    protected override bool CheckPrerequisites(PC computer)
    {
        // Унікальна вимога 1: наявність підключення до мережі
        if (!computer.IsConnectedToInternet)
        {
            NotifyStateChanged($"Помилка запуску: для гри в '{Title}' необхідне підключення до мережі Інтернет.");
            return false;
        }

        // Унікальна вимога 2: перевірка характеристик (онлайн-казино вимагає тільки наявність браузера, 
        // тому MinRequirements тут будуть мінімальними, але перевірити їх варто для загальної логіки)
        if (!computer.MeetsRequirements(MinRequirements))
        {
         NotifyStateChanged($"Помилка запуску: ПК не відповідає мінімальним системним вимогам для '{Title}'.");
         return false;
        }

        // Тут НЕМАЄ перевірки на IsInstalled, бо гра браузерна
        return true;
    }

    // Унікальний ігровий процес
    protected override void ExecutePlayAction()
    {
        NotifyStateChanged($"[Ігровий процес] Ви граєте в '{Title}': Ставите всі гроші на червоне, випадає зеро. Ви програли, але процес захоплює!");
    }
}