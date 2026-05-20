using System;
using System.Collections.Generic;
using System.Text;
namespace RecipePlanner.UI;

public static class ConsoleHelper
{
    public static void WriteHeader(string title)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(new string('=', 40));
        Console.WriteLine($" {title.ToUpper()}");
        Console.WriteLine(new string('=', 40));
        Console.ResetColor();
    }

    public static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ПОМИЛКА]: {message}");
        Console.ResetColor();
    }

    public static void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[УСПІХ]: {message}");
        Console.ResetColor();
    }

    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write($"{prompt} ({min}-{max}): ");
            if (int.TryParse(Console.ReadLine(), out int result) && result >= min && result <= max)
            {
                return result;
            }
            WriteError($"Будь ласка, введіть число від {min} до {max}.");
        }
    }
}