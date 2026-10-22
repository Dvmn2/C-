using System.Globalization;
using System.Text;

namespace Lab2;
// я 10
// о 12
// г 3
public static class Program
{
    /// <summary>
    /// Максимальное количество объектов, которое можно создать за один раз.
    /// </summary>
    private const int MaxCount = 100;

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

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
            Console.WriteLine("0 - Выход");

            int choice = ReadInt("Ваш выбор: ", 0, 6);
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
            }
            ReadOptionalString("Введите что либо чтобы продолжить: ");
        }
    }

    private static void RunTask13()
    {
        Console.WriteLine("--- Задача 1.3: Имена ---");

        int count = ReadInt("Сколько имён создать: ", 1, MaxCount);
        var names = new Name[count];
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Имя №{i + 1}:");
            names[i] = ReadName();
        }

        Console.WriteLine("Результат:");
        foreach (Name name in names)
        {
            Console.WriteLine(name);
        }
    }

    private static void RunTask22()
    {
        Console.WriteLine("--- Задача 2.2: Человек с именем ---");

        int count = ReadInt("Сколько людей создать: ", 1, MaxCount);
        var people = new Person[count];
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Человек №{i + 1}:");
            people[i] = ReadPerson();
        }

        Console.WriteLine("Результат:");
        foreach (Person person in people)
        {
            Console.WriteLine(person);
        }
    }

    private static void RunTask23()
    {
        Console.WriteLine("--- Задача 2.3: Человек с родителем ---");

        int count = ReadInt("Сколько людей создать: ", 1, MaxCount);
        var people = new Person[count];
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Человек №{i + 1}:");
            people[i] = ReadPerson();
        }

        Console.WriteLine();
        Console.WriteLine("Назначение отцов:");
        PrintPeopleList(people);
        for (int i = 0; i < people.Length; i++)
        {
            AssignFather(people, i);
        }

        Console.WriteLine();
        Console.WriteLine("Результат:");
        PrintPeopleList(people);

        while (ReadYesNo("Изменить отца у кого-нибудь (в том числе проверить защиту от цикла)? (д/н): "))
        {
            int number = ReadInt($"Номер человека (1-{people.Length}): ", 1, people.Length);
            AssignFather(people, number - 1);

            Console.WriteLine("Результат:");
            PrintPeopleList(people);
        }
    }

    private static void RunTask32()
    {
        Console.WriteLine("--- Задача 3.2: Ломаная ---");

        Point start = ReadPoint("Начальная точка (общая для обеих ломаных)");
        Point end = ReadPoint("Конечная точка (общая для обеих ломаных)");

        int firstCount = ReadInt("Сколько промежуточных точек у первой ломаной: ", 0, MaxCount);
        Point[] firstMiddle = ReadPoints(firstCount, "Промежуточная точка первой ломаной");

        int secondCount = ReadInt("Сколько промежуточных точек у второй ломаной: ", 0, MaxCount);
        Point[] secondMiddle = ReadPoints(secondCount, "Промежуточная точка второй ломаной");

        var first = new Polyline(BuildPath(start, firstMiddle, end));
        var second = new Polyline(BuildPath(start, secondMiddle, end));

        Console.WriteLine();
        Console.WriteLine($"Ломаная 1: {first}");
        Console.WriteLine($"Ломаная 2: {second}");

        Console.WriteLine("Сдвиг начала первой ломаной:");
        double dx = ReadDouble("  Сдвиг по X: ");
        double dy = ReadDouble("  Сдвиг по Y: ");

        first.Points[0].X += dx;
        first.Points[0].Y += dy;

        string shift = $"({dx.ToString(CultureInfo.InvariantCulture)};{dy.ToString(CultureInfo.InvariantCulture)})";
        Console.WriteLine($"После сдвига начала первой ломаной на {shift}:");
        Console.WriteLine($"Ломаная 1: {first}");
        Console.WriteLine($"Ломаная 2: {second}");
    }

    private static void RunTask49()
    {
        Console.WriteLine("--- Задача 4.9: Создаём Ломаную ---");

        int count = ReadInt("Сколько точек в ломаной (0 — создать без параметров): ", 0, MaxCount);

        Polyline polyline;
        if (count == 0)
        {
            polyline = new Polyline();
            Console.WriteLine($"Без параметров: {polyline}");
        }
        else
        {
            polyline = new Polyline(ReadPoints(count, "Точка"));
            Console.WriteLine($"С набором точек: {polyline}");
        }
    }

    private static void RunTask57()
    {
        Console.WriteLine("--- Задача 5.7: Длина Ломаной ---");

        int count = ReadInt("Сколько точек в ломаной: ", 0, MaxCount);
        var polyline = new Polyline(ReadPoints(count, "Точка"));

        Console.WriteLine(polyline);
        Console.WriteLine($"Длина: {polyline.GetLength():F2}");

        while (ReadYesNo("Добавить точки к ломаной? (д/н): "))
        {
            int addCount = ReadInt("Сколько точек добавить: ", 1, MaxCount);
            polyline.AddPoints(ReadPoints(addCount, "Добавляемая точка"));

            Console.WriteLine(polyline);
            Console.WriteLine($"Длина: {polyline.GetLength():F2}");
        }
    }

    private static void PrintPeopleList(Person[] people)
    {
        for (int i = 0; i < people.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {people[i]}");
        }
    }

    /// <summary>
    /// Запрашивает отца для человека с номером index. При ошибке (например, цикл) отец не меняется.
    /// </summary>
    private static void AssignFather(Person[] people, int index)
    {
        int number = ReadInt(
            $"  Отец для человека №{index + 1} (номер 1-{people.Length}, 0 — нет отца): ",
            0,
            people.Length);

        try
        {
            people[index].Father = number == 0 ? null : people[number - 1];
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message} Отец не изменён.");
        }
    }

    private static Point[] BuildPath(Point start, Point[] middle, Point end)
    {
        var path = new Point[middle.Length + 2];
        path[0] = start;
        Array.Copy(middle, 0, path, 1, middle.Length);
        path[path.Length - 1] = end;

        return path;
    }

    private static Name ReadName()
    {
        while (true)
        {
            string? lastName = ReadOptionalString("  Фамилия (Enter — не задана): ");
            string? firstName = ReadOptionalString("  Личное имя (Enter — не задано): ");
            string? middleName = ReadOptionalString("  Отчество (Enter — не задано): ");

            if (lastName != null || firstName != null || middleName != null)
            {
                return new Name(lastName, firstName, middleName);
            }

            Console.WriteLine("Ошибка: нужно задать хотя бы одну часть имени. Повторите ввод.");
        }
    }

    private static Person ReadPerson()
    {
        Name name = ReadName();
        int height = ReadInt("  Рост: ", 10, 300);
        return new Person(name, height);
    }

    private static Point ReadPoint(string title)
    {
        Console.WriteLine($"{title}:");
        double x = ReadDouble("  X: ");
        double y = ReadDouble("  Y: ");
        return new Point(x, y);
    }

    private static Point[] ReadPoints(int count, string title)
    {
        var points = new Point[count];
        for (int i = 0; i < count; i++)
        {
            points[i] = ReadPoint($"{title} №{i + 1}");
        }

        return points;
    }

    /// <summary>
    /// Читает строку из консоли. При закрытом потоке ввода (Ctrl+Z / Ctrl+D) завершает программу.
    /// </summary>
    private static string ReadLine()
    {
        string? line = Console.ReadLine();
        if (line == null)
        {
            Console.WriteLine();
            Console.WriteLine("Ввод завершён. Выход.");
            Environment.Exit(0);
        }

        return line;
    }

    private static string? ReadOptionalString(string prompt)
    {
        Console.Write(prompt);
        string input = ReadLine().Trim();
        return input.Length == 0 ? null : input;
    }

    private static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string answer = ReadLine().Trim().ToLowerInvariant();

            if (answer is "д" or "да" or "y" or "yes")
            {
                return true;
            }

            if (answer is "н" or "нет" or "n" or "no")
            {
                return false;
            }

            Console.WriteLine("Ошибка: введите «д» или «н».");
        }
    }

    private static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(ReadLine().Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int value)
                && value >= min && value <= max)
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
            string input = ReadLine().Trim().Replace(',', '.');
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                && double.IsFinite(value))
            {
                return value;
            }

            Console.WriteLine("Ошибка: введите число (например, 3.5).");
        }
    }
}
