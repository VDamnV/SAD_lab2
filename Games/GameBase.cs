using System;
using GameSimulator.Interfaces;
using GameSimulator.Models;
using GameSimulator.Events;

namespace GameSimulator.Games;

public abstract class GameBase : IGame, ISaveable
{
    public string Title { get; }
    public string Genre { get; }
    public SystemRequirements MinRequirements { get; }
    public bool IsRunning { get; protected set; }
    public bool HasSavedState { get; protected set; }

    // 1. Класична реалізація події за допомогою стандартного делегата EventHandler (Шаблон Observer)
    public event EventHandler<GameStateEventArgs>? GameStateChanged;

    protected GameBase(string title, string genre, SystemRequirements minRequirements)
    {
        Title = title;
        Genre = genre;
        MinRequirements = minRequirements;
    }

    // 2. Метод-обгортка для безпечної генерації події та сповіщення підписників (Спостерігачів)
    protected virtual void NotifyStateChanged(string message)
    {
        // Ключова зміна для Лаб 1.3: передаємо 'this' (хто викликав подію) 
        // та новий об'єкт наших кастомних аргументів GameStateEventArgs
        GameStateChanged?.Invoke(this, new GameStateEventArgs(Title, message));
    }

    // Паттерн Template Method залишається
    public virtual void Start(PC computer)
    {
        if (IsRunning)
        {
            NotifyStateChanged("Гра вже запущена.");
            return;
        }

        // Тут буде викликатись делегований метод комп'ютера, що відповідає Закону Деметри (LoD)
        if (!CheckPrerequisites(computer))
        {
            NotifyStateChanged("Запуск скасовано: не виконані системні вимоги або передумови.");
            return;
        }

        IsRunning = true;
        NotifyStateChanged("Гру успішно запущено.");
    }

    public virtual void Stop()
    {
        if (!IsRunning) return;
        IsRunning = false;
        NotifyStateChanged("Гру зупинено.");
    }

    public virtual void Save()
    {
        if (!IsRunning)
        {
            NotifyStateChanged("Помилка збереження: неможливо використовувати функціонал, доки гру не запущено.");
            return;
        }
        HasSavedState = true;
        NotifyStateChanged("Поточний стан гри збережено.");
    }

    public virtual void Load()
    {
        if (!IsRunning)
        {
            NotifyStateChanged("Помилка завантаження: неможливо використовувати функціонал, доки гру не запущено.");
            return;
        }
        if (!HasSavedState)
        {
            NotifyStateChanged("Помилка: немає збережених станів для завантаження.");
            return;
        }
        NotifyStateChanged("Збережений стан успішно завантажено.");
    }

    public void Play()
    {
        if (!IsRunning)
        {
            NotifyStateChanged("Помилка: неможливо грати в незапущену гру.");
            return;
        }
        ExecutePlayAction();
    }

    // --- АБСТРАКТНІ МЕТОДИ ---
    protected abstract bool CheckPrerequisites(PC computer);
    protected abstract void ExecutePlayAction();
}