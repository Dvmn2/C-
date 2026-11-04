using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Lab3;

/// <summary>
/// Задания 4-8 (вариант 10): работа с бинарными и текстовыми файлами.
/// </summary>
public static class FileTasks
{
    private static readonly XmlSerializer ToySerializer = new XmlSerializer(typeof(Toy));
    private static readonly Encoding TextEncoding = new UTF8Encoding(false);
    private static readonly char[] Separators = { ' ', '\t' };

    private static readonly string[] ToyNames =
    {
        "Мяч", "Кукла", "Конструктор", "Кубики", "Машинка", "Пазл", "Робот", "Мишка"
    };

    private static readonly string[] SampleLines =
    {
        "Сегодня старшеклассники выполняли ЕГЭ по информатике и ИКТ",
        "Hello, world!",
        "",
        "Лабораторная работа №3",
        "Последняя строка файла"
    };

    // ---------- Задание 4. Бинарные файлы с числами ----------

    /// <summary>
    /// Заполняет бинарный файл случайными целыми числами из [minValue; maxValue].
    /// </summary>
    public static void FillBinaryFile(string path, int count, int minValue, int maxValue, Random random)
    {
        using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(random.Next(minValue, maxValue + 1));
            }
        }
    }

    public static List<int> ReadBinaryInts(string path)
    {
        List<int> numbers = new List<int>();

        using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(stream))
        {
            while (stream.Position < stream.Length)
            {
                numbers.Add(reader.ReadInt32());
            }
        }

        return numbers;
    }

    public static void PrintBinaryFile(string path)
    {
        List<int> numbers = ReadBinaryInts(path);
        Console.WriteLine(numbers.Count == 0 ? "(файл пуст)" : string.Join(" ", numbers));
    }

    /// <summary>
    /// Количество удвоенных нечетных чисел, то есть чисел вида 2k, где k нечетно.
    /// </summary>
    public static int CountDoubledOdd(string path)
    {
        List<int> numbers = ReadBinaryInts(path);
        int count = 0;

        for (int i = 0; i < numbers.Count; i++)
        {
            int value = numbers[i];
            if (value % 2 == 0 && (value / 2) % 2 != 0)
            {
                count++;
            }
        }

        return count;
    }

    // ---------- Задание 5. Бинарные файлы со структурами (XML-сериализация) ----------

    /// <summary>
    /// Заполняет файл игрушками, введенными с клавиатуры.
    /// </summary>
    public static void FillToysFileFromKeyboard(string path)
    {
        int count = ConsoleInput.ReadInt("Сколько игрушек записать в файл (1-20): ", 1, 20);
        List<Toy> toys = new List<Toy>();

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Игрушка {i + 1}:");
            string name = ConsoleInput.ReadNonEmptyString("  Название: ");
            double price = ConsoleInput.ReadDouble("  Стоимость, руб.: ", 0, 1000000);
            int minAge = ConsoleInput.ReadInt("  Минимальный возраст: ", 0, 18);
            int maxAge = ConsoleInput.ReadInt("  Максимальный возраст: ", minAge, 18);
            toys.Add(new Toy(name, price, minAge, maxAge));
        }

        WriteToys(path, toys);
    }

    /// <summary>
    /// Заполняет файл случайными игрушками.
    /// </summary>
    public static void FillToysFileRandom(string path, int count, Random random)
    {
        List<Toy> toys = new List<Toy>();

        for (int i = 0; i < count; i++)
        {
            string name = ToyNames[random.Next(ToyNames.Length)];
            double price = random.Next(50, 5001);
            int minAge = random.Next(0, 8);
            int maxAge = minAge + random.Next(1, 8);
            toys.Add(new Toy(name, price, minAge, maxAge));
        }

        WriteToys(path, toys);
    }

    private static void WriteToys(string path, List<Toy> toys)
    {
        using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            for (int i = 0; i < toys.Count; i++)
            {
                using (StringWriter xmlWriter = new StringWriter(CultureInfo.InvariantCulture))
                {
                    ToySerializer.Serialize(xmlWriter, toys[i]);
                    writer.Write(xmlWriter.ToString());
                }
            }
        }
    }

    public static List<Toy> ReadToys(string path)
    {
        List<Toy> toys = new List<Toy>();

        using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(stream))
        {
            while (stream.Position < stream.Length)
            {
                using (StringReader xmlReader = new StringReader(reader.ReadString()))
                {
                    toys.Add((Toy)ToySerializer.Deserialize(xmlReader));
                }
            }
        }

        return toys;
    }

    public static void PrintToysFile(string path)
    {
        List<Toy> toys = ReadToys(path);

        if (toys.Count == 0)
        {
            Console.WriteLine("(файл пуст)");
        }

        for (int i = 0; i < toys.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {toys[i]}");
        }
    }

    /// <summary>
    /// Находит самую дешевую игрушку. Возвращает false, если файл пуст.
    /// </summary>
    public static bool TryFindCheapestToy(string path, out Toy cheapest)
    {
        List<Toy> toys = ReadToys(path);
        cheapest = new Toy();

        if (toys.Count == 0)
        {
            return false;
        }

        cheapest = toys[0];
        for (int i = 1; i < toys.Count; i++)
        {
            if (toys[i].Price < cheapest.Price)
            {
                cheapest = toys[i];
            }
        }

        return true;
    }

    // ---------- Задание 6. Текстовый файл, одно число в строке ----------

    public static void FillTextFileOnePerLine(string path, int count, int minValue, int maxValue, Random random)
    {
        using (StreamWriter writer = new StreamWriter(path, false, TextEncoding))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(random.Next(minValue, maxValue + 1).ToString(CultureInfo.InvariantCulture));
            }
        }
    }

    public static List<int> ReadIntsOnePerLine(string path)
    {
        string[] lines = File.ReadAllLines(path, TextEncoding);
        List<int> numbers = new List<int>();

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (line.Length == 0)
            {
                continue;
            }

            numbers.Add(ParseInt(line, i + 1));
        }

        return numbers;
    }

    /// <summary>
    /// Квадрат разности максимального и минимального элементов файла.
    /// Возвращает false, если в файле нет чисел.
    /// </summary>
    public static bool TryGetSquaredRange(string path, out decimal result)
    {
        List<int> numbers = ReadIntsOnePerLine(path);
        result = 0;

        if (numbers.Count == 0)
        {
            return false;
        }

        int min = numbers[0];
        int max = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] < min)
            {
                min = numbers[i];
            }

            if (numbers[i] > max)
            {
                max = numbers[i];
            }
        }

        decimal difference = (decimal)max - min;
        result = difference * difference;
        return true;
    }

    // ---------- Задание 7. Текстовый файл, несколько чисел в строке ----------

    public static void FillTextFileSeveralPerLine(
        string path, int lineCount, int maxPerLine, int minValue, int maxValue, Random random)
    {
        using (StreamWriter writer = new StreamWriter(path, false, TextEncoding))
        {
            for (int i = 0; i < lineCount; i++)
            {
                int numbersInLine = random.Next(1, maxPerLine + 1);
                StringBuilder builder = new StringBuilder();

                for (int j = 0; j < numbersInLine; j++)
                {
                    if (j > 0)
                    {
                        builder.Append(' ');
                    }

                    builder.Append(random.Next(minValue, maxValue + 1).ToString(CultureInfo.InvariantCulture));
                }

                writer.WriteLine(builder.ToString());
            }
        }
    }

    public static List<int> ReadIntsSeveralPerLine(string path)
    {
        string[] lines = File.ReadAllLines(path, TextEncoding);
        List<int> numbers = new List<int>();

        for (int i = 0; i < lines.Length; i++)
        {
            string[] tokens = lines[i].Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < tokens.Length; j++)
            {
                numbers.Add(ParseInt(tokens[j], i + 1));
            }
        }

        return numbers;
    }

    /// <summary>
    /// Сумма нечетных элементов файла. Возвращает false, если в файле нет чисел.
    /// </summary>
    public static bool TrySumOdd(string path, out long sum)
    {
        List<int> numbers = ReadIntsSeveralPerLine(path);
        sum = 0;

        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] % 2 != 0)
            {
                sum += numbers[i];
            }
        }

        return numbers.Count > 0;
    }

    // ---------- Задание 8. Текстовый файл с текстом ----------

    /// <summary>
    /// Заполняет файл строками, введенными с клавиатуры. Ввод заканчивается строкой /end.
    /// </summary>
    public static void FillTextFileFromKeyboard(string path)
    {
        Console.WriteLine("Вводите строки текста. Для завершения введите /end");
        List<string> lines = new List<string>();

        while (true)
        {
            string line = Console.ReadLine();
            if (line == null || line == "/end")
            {
                break;
            }

            lines.Add(line);
        }

        File.WriteAllLines(path, lines, TextEncoding);
    }

    public static void FillTextFileWithSample(string path)
    {
        File.WriteAllLines(path, SampleLines, TextEncoding);
    }

    public static void PrintTextFile(string path)
    {
        string[] lines = File.ReadAllLines(path, TextEncoding);

        if (lines.Length == 0)
        {
            Console.WriteLine("(файл пуст)");
        }

        for (int i = 0; i < lines.Length; i++)
        {
            Console.WriteLine(lines[i]);
        }
    }

    /// <summary>
    /// Создает новый файл, каждая строка которого содержит последний символ
    /// соответствующей строки исходного файла (для пустой строки - пустая строка).
    /// </summary>
    public static void CreateLastCharFile(string sourcePath, string targetPath)
    {
        string[] lines = File.ReadAllLines(sourcePath, TextEncoding);
        string[] result = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            result[i] = lines[i].Length == 0 ? string.Empty : lines[i][lines[i].Length - 1].ToString();
        }

        File.WriteAllLines(targetPath, result, TextEncoding);
    }

    private static int ParseInt(string token, int lineNumber)
    {
        int value;

        if (!int.TryParse(token, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value))
        {
            throw new FormatException($"Строка {lineNumber}: \"{token}\" не является целым числом.");
        }

        return value;
    }
}
