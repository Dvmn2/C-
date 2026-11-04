using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Lab3;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Random random = new Random();
        bool exit = false;

        while (!exit)
        {
            PrintMenu();
            int choice = ConsoleInput.ReadInt("Ваш выбор: ", 0, 8);
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        RunTask1(random);
                        break;
                    case 2:
                        RunTask2();
                        break;
                    case 3:
                        RunTask3(random);
                        break;
                    case 4:
                        RunTask4(random);
                        break;
                    case 5:
                        RunTask5(random);
                        break;
                    case 6:
                        RunTask6(random);
                        break;
                    case 7:
                        RunTask7(random);
                        break;
                    case 8:
                        RunTask8();
                        break;
                }
            }
            catch (IOException exception)
            {
                Console.WriteLine($"Ошибка работы с файлом: {exception.Message}");
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine($"Нет доступа к файлу: {exception.Message}");
            }
            catch (FormatException exception)
            {
                Console.WriteLine($"Неверный формат данных в файле: {exception.Message}");
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine($"Файл содержит некорректные данные: {exception.Message}");
            }
            catch (XmlException exception)
            {
                Console.WriteLine($"Ошибка XML: {exception.Message}");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Ошибка: {exception.Message}");
            }

            if (!exit)
            {
                Console.WriteLine();
            }
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("Лабораторная работа №3 (вариант 10)");
        Console.WriteLine("1 - Задание 1. Заполнение двумерных массивов");
        Console.WriteLine("2 - Задание 2. Элементы, большие любого элемента главной диагонали");
        Console.WriteLine("3 - Задание 3. Матричное выражение A*C^T + B*C");
        Console.WriteLine("4 - Задание 4. Бинарные файлы: удвоенные нечетные числа");
        Console.WriteLine("5 - Задание 5. Бинарные файлы и структуры: самая дешевая игрушка");
        Console.WriteLine("6 - Задание 6. Текстовый файл: квадрат разности max и min");
        Console.WriteLine("7 - Задание 7. Текстовый файл: сумма нечетных элементов");
        Console.WriteLine("8 - Задание 8. Текстовый файл: последний символ каждой строки");
        Console.WriteLine("0 - Выход");
    }

    private static void RunTask1(Random random)
    {
        int n = ConsoleInput.ReadInt("Введите n (1-10): ", 1, 10);
        int m = ConsoleInput.ReadInt("Введите m (1-10): ", 1, 10);

        Matrix first = new Matrix(n, m);
        Matrix second = new Matrix(n, random);
        Matrix third = new Matrix(n);

        Console.WriteLine("Первый массив:");
        Console.WriteLine(first);
        Console.WriteLine("Второй массив:");
        Console.WriteLine(second);
        Console.WriteLine("Третий массив:");
        Console.WriteLine(third);
    }

    private static void RunTask2()
    {
        int n = ConsoleInput.ReadInt("Введите n (1-10): ", 1, 10);

        Matrix matrix = new Matrix(n, n);
        Console.WriteLine("Массив:");
        Console.WriteLine(matrix);
        Console.WriteLine($"Элементов, больших любого элемента главной диагонали: {matrix.CountGreaterThanMainDiagonal()}");
    }

    private static void RunTask3(Random random)
    {
        Console.WriteLine("Для выражения A*C^T + B*C все матрицы должны быть квадратными размера n x n.");
        int n = ConsoleInput.ReadInt("Введите n (1-10): ", 1, 10);

        Console.WriteLine("Ввод матрицы A:");
        Matrix a = new Matrix(n, n);
        Matrix b = new Matrix(n, random);
        Matrix c = new Matrix(n);

        Matrix result = a * c.Transpose() + b * c;

        Console.WriteLine("A:");
        Console.WriteLine(a);
        Console.WriteLine("B:");
        Console.WriteLine(b);
        Console.WriteLine("C:");
        Console.WriteLine(c);
        Console.WriteLine("Результат A*C^T + B*C:");
        Console.WriteLine(result);
    }

    private static void RunTask4(Random random)
    {
        string path = ConsoleInput.ReadFilePath("Имя бинарного файла [numbers.bin]: ", "numbers.bin");

        if (AskToFill())
        {
            int count = ConsoleInput.ReadInt("Количество чисел (1-100): ", 1, 100);
            FileTasks.FillBinaryFile(path, count, -100, 100, random);
        }
        else if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        Console.WriteLine("Содержимое файла:");
        FileTasks.PrintBinaryFile(path);
        Console.WriteLine($"Количество удвоенных нечетных чисел: {FileTasks.CountDoubledOdd(path)}");
    }

    private static void RunTask5(Random random)
    {
        string path = ConsoleInput.ReadFilePath("Имя бинарного файла [toys.bin]: ", "toys.bin");

        if (AskToFill())
        {
            int source = ConsoleInput.ReadInt("1 - ввод с клавиатуры, 2 - случайные данные: ", 1, 2);
            if (source == 1)
            {
                FileTasks.FillToysFileFromKeyboard(path);
            }
            else
            {
                int count = ConsoleInput.ReadInt("Количество игрушек (1-20): ", 1, 20);
                FileTasks.FillToysFileRandom(path, count, random);
            }
        }
        else if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        Console.WriteLine("Содержимое файла:");
        FileTasks.PrintToysFile(path);

        Toy cheapest;
        if (FileTasks.TryFindCheapestToy(path, out cheapest))
        {
            Console.WriteLine($"Самая дешевая игрушка: {cheapest.Name}");
        }
        else
        {
            Console.WriteLine("В файле нет игрушек.");
        }
    }

    private static void RunTask6(Random random)
    {
        string path = ConsoleInput.ReadFilePath("Имя текстового файла [numbers6.txt]: ", "numbers6.txt");

        if (AskToFill())
        {
            int count = ConsoleInput.ReadInt("Количество чисел (1-100): ", 1, 100);
            FileTasks.FillTextFileOnePerLine(path, count, -100, 100, random);
        }
        else if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        Console.WriteLine("Содержимое файла:");
        FileTasks.PrintTextFile(path);

        decimal result;
        if (FileTasks.TryGetSquaredRange(path, out result))
        {
            Console.WriteLine($"Квадрат разности максимального и минимального элементов: {result}");
        }
        else
        {
            Console.WriteLine("В файле нет чисел.");
        }
    }

    private static void RunTask7(Random random)
    {
        string path = ConsoleInput.ReadFilePath("Имя текстового файла [numbers7.txt]: ", "numbers7.txt");

        if (AskToFill())
        {
            int lineCount = ConsoleInput.ReadInt("Количество строк (1-20): ", 1, 20);
            int maxPerLine = ConsoleInput.ReadInt("Максимум чисел в строке (1-10): ", 1, 10);
            FileTasks.FillTextFileSeveralPerLine(path, lineCount, maxPerLine, -100, 100, random);
        }
        else if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        Console.WriteLine("Содержимое файла:");
        FileTasks.PrintTextFile(path);

        long sum;
        if (FileTasks.TrySumOdd(path, out sum))
        {
            Console.WriteLine($"Сумма нечетных элементов: {sum}");
        }
        else
        {
            Console.WriteLine("В файле нет чисел.");
        }
    }

    private static void RunTask8()
    {
        string source = ConsoleInput.ReadFilePath("Имя исходного файла [text.txt]: ", "text.txt");
        string target = ConsoleInput.ReadFilePath("Имя нового файла [result.txt]: ", "result.txt");

        if (string.Equals(source, target, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Ошибка: имена файлов должны различаться.");
            return;
        }

        int choice = ConsoleInput.ReadInt(
            "1 - ввести текст с клавиатуры, 2 - записать пример текста, 3 - использовать существующий файл: ", 1, 3);

        if (choice == 1)
        {
            FileTasks.FillTextFileFromKeyboard(source);
        }
        else if (choice == 2)
        {
            FileTasks.FillTextFileWithSample(source);
        }
        else if (!File.Exists(source))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        FileTasks.CreateLastCharFile(source, target);

        Console.WriteLine("Исходный файл:");
        FileTasks.PrintTextFile(source);
        Console.WriteLine("Новый файл (последние символы строк):");
        FileTasks.PrintTextFile(target);
    }

    private static bool AskToFill()
    {
        int choice = ConsoleInput.ReadInt(
            "1 - заполнить файл новыми данными, 2 - использовать существующий файл: ", 1, 2);
        return choice == 1;
    }
}
