using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine(" ");
Console.WriteLine("   Синхпонне та асинхронне виконання");
Console.WriteLine(" ");

Console.WriteLine("\n синхронне виконання");

Stopwatch syncTimer = Stopwatch.StartNew();

try
{
    string result1 = FirstMethod(5);
    Console.WriteLine(result1);

    string result2 = SecondMethod(10);
    Console.WriteLine(result2);
}
catch (Exception ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
}

syncTimer.Stop();

Console.WriteLine($"Час синхронного виконання: {syncTimer.ElapsedMilliseconds} мс");


Console.WriteLine("\n Асинхронне виконання");

Stopwatch asyncTimer = Stopwatch.StartNew();

try
{
    Task<string> task1 = FirstMethodAsync(5);
    Task<string> task2 = SecondMethodAsync(10);

    string[] results = await Task.WhenAll(task1, task2);

    foreach (string result in results)
    {
        Console.WriteLine(result);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
}

asyncTimer.Stop();

Console.WriteLine($"Час асинхронного виконання: {asyncTimer.ElapsedMilliseconds} мс");

Console.WriteLine("\nПеревірка обробки помилок");

try
{
    string result = await FirstMethodAsync(-5);
    Console.WriteLine(result);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Перехоплено виняток: {ex.Message}");
}


Console.WriteLine("\nАналіз ефективності");

double syncTime = syncTimer.ElapsedMilliseconds;
double asyncTime = asyncTimer.ElapsedMilliseconds;

Console.WriteLine($"Синхронно: {syncTime} мс");
Console.WriteLine($"Асинхронно: {asyncTime} мс");

if (asyncTime > 0)
{
    double difference = syncTime / asyncTime;

    Console.WriteLine($"Відношення часу виконання: {difference:F2} разів");
}

Console.WriteLine("\nПрограму завершено!");


string FirstMethod(int number)
{
    if (number < 0)
        throw new ArgumentException("Число не може бути від'ємним!");

    Console.WriteLine("Перший синхронний метод розпочав роботу...");

    Thread.Sleep(2000);

    int result = number * number;

    return $"Перший метод: квадрат числа {number} = {result}";
}

string SecondMethod(int number)
{
    if (number < 0)
        throw new ArgumentException("Число не може бути від'ємним!");

    Console.WriteLine("Другий синхронний метод розпочав роботу...");

    Thread.Sleep(2000);

    int result = number * 2;

    return $"Другий метод: подвоєне число {number} = {result}";
}

async Task<string> FirstMethodAsync(int number)
{
    if (number < 0)
        throw new ArgumentException("Число не може бути від'ємним!");

    Console.WriteLine("Перший асинхронний метод розпочав роботу...");

    await Task.Delay(2000);

    int result = number * number;

    return $"Перший асинхронний метод: квадрат числа {number} = {result}";
}

async Task<string> SecondMethodAsync(int number)
{
    if (number < 0)
        throw new ArgumentException("Число не може бути від'ємним!");

    Console.WriteLine("Другий асинхронний метод розпочав роботу...");

    await Task.Delay(2000);

    int result = number * 2;

    return $"Другий асинхронний метод: подвоєне число {number} = {result}";
}