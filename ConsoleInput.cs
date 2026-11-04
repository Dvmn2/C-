using System;
using System.Globalization;
using System.IO;

namespace Lab3;

/// <summary>
/// Ввод данных с клавиатуры с проверкой корректности.
/// </summary>
public static class ConsoleInput
{
    /// <summary>
    /// Запрашивает целое число из диапазона [min; max], пока не будет введено корректное значение.
    /// </summary>
    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string line = Console.ReadLine();
            int value;

            if (line != null
                && int.TryParse(line.Trim(), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value)
                && value >= min
                && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Ошибка: нужно ввести целое число от {min} до {max}.");
        }
    }

    /// <summary>
    /// Запрашивает вещественное число из диапазона [min; max]. Допускается запятая или точка.
    /// </summary>
    public static double ReadDouble(string prompt, double min, double max)
    {
        while (true)
        {
            Console.Write(prompt);
            string line = Console.ReadLine();
            double value;

            if (line != null
                && double.TryParse(line.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out value)
                && value >= min
                && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Ошибка: нужно ввести число от {min} до {max}.");
        }
    }

    /// <summary>
    /// Запрашивает непустую строку.
    /// </summary>
    public static string ReadNonEmptyString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string line = Console.ReadLine();

            if (line != null && line.Trim().Length > 0)
            {
                return line.Trim();
            }

            Console.WriteLine("Ошибка: строка не должна быть пустой.");
        }
    }

    /// <summary>
    /// Запрашивает имя файла. Пустой ввод означает значение по умолчанию.
    /// </summary>
    public static string ReadFilePath(string prompt, string defaultPath)
    {
        while (true)
        {
            Console.Write(prompt);
            string line = Console.ReadLine();

            if (line == null || line.Trim().Length == 0)
            {
                return defaultPath;
            }

            string path = line.Trim();

            if (path.IndexOfAny(Path.GetInvalidPathChars()) < 0
                && path.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)
            {
                return path;
            }

            Console.WriteLine("Ошибка: имя файла содержит недопустимые символы.");
        }
    }
}
