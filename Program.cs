using System;
using GameSimulator.Models;
using GameSimulator.Games;
using GameSimulator.UI;
using GameSimulator.Factories;
using GameSimulator.Interfaces;

// Налаштування консолі
Console.OutputEncoding = System.Text.Encoding.UTF8;
ConsoleLogger.LogMessage("=== СИМУЛЯТОР ІГОР ЗАПУЩЕНО (Лабораторна 1.3) ===\n");

// 1. Створюємо ПК гравця
var myPcSpecs = new SystemRequirements { CpuCores = 8, RamMB = 16384, GpuMemoryMB = 8192, FreeHddSpaceMB = 500000 };
var myPc = new PC(myPcSpecs, hasInternet: true);

// 2. Створюємо ігри через ШАБЛОН FACTORY METHOD
var ck3 = GameFactory.CreateGame(GameType.Strategy, "Crusader Kings III", new SystemRequirements { CpuCores = 4, RamMB = 8192, GpuMemoryMB = 4096 }, 10000);
var assetto = GameFactory.CreateGame(GameType.Simulator, "Assetto Corsa", new SystemRequirements { CpuCores = 4, RamMB = 8192, GpuMemoryMB = 2048 }, 15000);
var casino = GameFactory.CreateGame(GameType.Casino, "NoWin Casino", new SystemRequirements { CpuCores = 2, RamMB = 4096, GpuMemoryMB = 512 });

// 3. Підписуємось на події через ШАБЛОН OBSERVER
ck3.GameStateChanged += ConsoleLogger.LogGameStateChange;
assetto.GameStateChanged += ConsoleLogger.LogGameStateChange;
casino.GameStateChanged += ConsoleLogger.LogGameStateChange;

// 4. Створюємо гравця та додаємо ігри
var player = new Player("Студент");
player.AddGame(ck3);
player.AddGame(assetto);
player.AddGame(casino);

Console.WriteLine("\n[Натисніть Enter, щоб розпочати демонстрацію...]");
Console.ReadLine();

// --- СЦЕНАРІЙ 1 ---
Console.WriteLine("\n--- СЦЕНАРІЙ 1: Спроба запуску без інсталяції ---");
player.StartGame(ck3, myPc); 
Console.WriteLine("\n[Натисніть Enter для переходу до наступного кроку...]");
Console.ReadLine();

// --- СЦЕНАРІЙ 2 ---
Console.WriteLine("\n--- СЦЕНАРІЙ 2: Інсталяція, запуск та збереження гри ---");
if (ck3 is IInstallable installableCk3)
{
    installableCk3.Install(myPc.FreeHddSpaceMB); // Закон Деметри у дії
}
player.StartGame(ck3, myPc);
ck3.Play();
if (ck3 is ISaveable saveableCk3) saveableCk3.Save();
Console.WriteLine("\n[Натисніть Enter для переходу до наступного кроку...]");
Console.ReadLine();

// --- СЦЕНАРІЙ 3 ---
Console.WriteLine("\n--- СЦЕНАРІЙ 3: Блокування одночасного запуску двох ігор ---");
player.StartGame(assetto, myPc); // Має видати помилку, бо CK3 ще запущена
Console.WriteLine("\n[Натисніть Enter для переходу до наступного кроку...]");
Console.ReadLine();

// --- СЦЕНАРІЙ 4 ---
Console.WriteLine("\n--- СЦЕНАРІЙ 4: Робота з периферією (кермо у симуляторі) ---");
ck3.Stop(); // Звільняємо ПК

if (assetto is IInstallable installableAssetto)
{
    installableAssetto.Install(myPc.FreeHddSpaceMB);
}

// Перевіряємо, чи це дійсно симулятор, щоб підключити кермо
if (assetto is SimulatorGame simGame)
{
    var logitechWheel = new SteeringWheel("Logitech G29");
    simGame.ConnectSteeringWheel(logitechWheel);
}

player.StartGame(assetto, myPc);
assetto.Play(); // Граємо з кермом

if (assetto is SimulatorGame simGameToDisconnect)
{
    simGameToDisconnect.DisconnectSteeringWheel();
}
assetto.Play(); // Граємо без керма
assetto.Stop();
Console.WriteLine("\n[Натисніть Enter для переходу до наступного кроку...]");
Console.ReadLine();

// --- СЦЕНАРІЙ 5 ---
Console.WriteLine("\n--- СЦЕНАРІЙ 5: Браузерна онлайн-гра (без інсталяції) ---");
// Казино не реалізує IInstallable, тому одразу запускаємо
player.StartGame(casino, myPc); 
casino.Play();
casino.Stop();
Console.WriteLine("\n[Натисніть Enter для завершення...]");
Console.ReadLine();

ConsoleLogger.LogMessage("\n=== ДЕМОНСТРАЦІЮ УСПІШНО ЗАВЕРШЕНО ===");