using System.Globalization;
using System.Text;

namespace Lab2;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Лабораторная работа №2, вариант 10 ===");
            Console.WriteLine("1 - Задача 1.3: Имена");
            Console.WriteLine("2 - Задача 2.2: Человек с именем");
            Console.WriteLine("3 - Задача 2.3: Человек с родителем");
            Console.WriteLine("4 - Задача 3.2: Ломаная");
            Console.WriteLine("5 - Задача 4.9: Создаём Ломаную");
            Console.WriteLine("6 - Задача 5.7: Длина Ломаной");
            Console.WriteLine("7 - Выполнить все задачи подряд");
            Console.WriteLine("0 - Выход");

            int choice = ReadInt("Ваш выбор: ", 0, 7);
            Console.WriteLine();

            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    RunTask13();
                    break;
                case 2:
                    RunTask22();
                    break;
                case 3:
                    RunTask23();
                    break;
                case 4:
                    RunTask32();
                    break;
                case 5:
                    RunTask49();
                    break;
                case 6:
                    RunTask57();
                    break;
                default:
                    RunTask13();
                    RunTask22();
                    RunTask23();
                    RunTask32();
                    RunTask49();
                    RunTask57();
                    break;
            }
        }
    }

    private static void RunTask13()
    {
        Console.WriteLine("--- Задача 1.3: Имена ---");

        var names = new[]
        {
            new Name(firstName: "Клеопатра"),
            new Name("Пушкин", "Александр", "Сергеевич"),
            new Name("Маяковский", "Владимир"),
        };

        foreach (Name name in names)
        {
            Console.WriteLine(name);
        }
    }

    private static void RunTask22()
    {
        Console.WriteLine("--- Задача 2.2: Человек с именем ---");

        var people = new[]
        {
            new Person(new Name(firstName: "Клеопатра"), 152),
            new Person(new Name("Пушкин", "Александр", "Сергеевич"), 167),
            new Person(new Name("Маяковский", "Владимир"), 189),
        };

        foreach (Person person in people)
        {
            Console.WriteLine(person);
        }
    }

    private static void RunTask23()
    {
        Console.WriteLine("--- Задача 2.3: Человек с родителем ---");

        var ivan = new Person(new Name("Чудов", "Иван"), 180);
        var petr = new Person(new Name("Чудов", "Пётр"), 178);
        var boris = new Person(new Name(firstName: "Борис"), 175);

        ivan.Father = null;
        petr.Father = ivan;
        boris.Father = petr;

        Console.WriteLine(ivan);
        Console.WriteLine(petr);
        Console.WriteLine(boris);

        // Проверка защиты от циклов.
        try
        {
            ivan.Father = boris;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void RunTask32()
    {
        Console.WriteLine("--- Задача 3.2: Ломаная ---");

        var start = new Point(1, 5);
        var end = new Point(5, 3);

        var first = new Polyline(start, new Point(2, 8), end);
        var second = new Polyline(start, new Point(2, -5), new Point(4, -8), end);

        Console.WriteLine($"Ломаная 1: {first}");
        Console.WriteLine($"Ломаная 2: {second}");

        // Начало общее для обеих ломаных (одна и та же Точка), поэтому сдвигаются обе.
        first.Points[0].X += 10;
        first.Points[0].Y += 10;

        Console.WriteLine("После сдвига начала первой ломаной на (10;10):");
        Console.WriteLine($"Ломаная 1: {first}");
        Console.WriteLine($"Ломаная 2: {second}");
    }

    private static void RunTask49()
    {
        Console.WriteLine("--- Задача 4.9: Создаём Ломаную ---");

        var empty = new Polyline();
        var withPoints = new Polyline(new Point(0, 0), new Point(3, 4));

        Console.WriteLine($"Без параметров: {empty}");
        Console.WriteLine($"С набором точек: {withPoints}");
    }

    private static void RunTask57()
    {
        Console.WriteLine("--- Задача 5.7: Длина Ломаной ---");

        var polyline = new Polyline(new Point(1, 5), new Point(2, 8), new Point(5, 3));
        Console.WriteLine(polyline);
        Console.WriteLine($"Длина: {polyline.GetLength():F2}");

        polyline.AddPoints(new Point(5, 15), new Point(8, 10));
        Console.WriteLine("После добавления точек {5;15}, {8;10}:");
        Console.WriteLine(polyline);
        Console.WriteLine($"Длина: {polyline.GetLength():F2}");

        Console.Write("Добавить свою точку? (д/н): ");
        string? answer = Console.ReadLine()?.Trim().ToLowerInvariant();
        if (answer is "д" or "y" or "да")
        {
            double x = ReadDouble("X: ");
            double y = ReadDouble("Y: ");
            polyline.AddPoints(new Point(x, y));
            Console.WriteLine(polyline);
            Console.WriteLine($"Длина: {polyline.GetLength():F2}");
        }
    }

    private static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Ошибка: введите целое число от {min} до {max}.");
        }
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim().Replace(',', '.');
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                return value;
            }

            Console.WriteLine("Ошибка: введите число (например, 3.5).");
        }
    }
}
