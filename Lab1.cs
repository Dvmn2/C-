using System.Globalization;
using System.Text;

// Лабораторная работа №1. Решены нечётные задачи (1, 3, 5, 7, 9) из каждого задания.
public class Lab1
{
    // ======================= ЗАДАНИЕ 1. МЕТОДЫ =======================

    // 1.1 Дробная часть
    public double fraction(double x)
    {
        return x - (long)x; // (long)x отбрасывает дробную часть
    }

    // 1.3 Букву в число
    public int charToNum(char x)
    {
        return x - '0'; // код '0' = 48, '3' = 51 -> 51 - 48 = 3
    }

    // 1.5 Двузначное
    public bool is2Digits(int x)
    {
        return (x >= 10 && x <= 99) || (x <= -10 && x >= -99);
    }

    // 1.7 Диапазон (порядок a и b неизвестен)
    public bool isInRange(int a, int b, int num)
    {
        int left = Math.Min(a, b);
        int right = Math.Max(a, b);
        return num >= left && num <= right;
    }

    // 1.9 Равенство
    public bool isEqual(int a, int b, int c)
    {
        return a == b && b == c;
    }

    // ======================= ЗАДАНИЕ 2. УСЛОВИЯ =======================

    // 2.1 Модуль числа
    public int abs(int x)
    {
        if (x < 0)
        {
            return -x;
        }
        return x;
    }

    // 2.3 Тридцать пять
    public bool is35(int x)
    {
        bool by3 = x % 3 == 0;
        bool by5 = x % 5 == 0;
        if (by3 && by5)
        {
            return false;
        }
        return by3 || by5;
    }

    // 2.5 Тройной максимум (две инструкции if, без вложенных)
    public int max3(int x, int y, int z)
    {
        int max = x;
        if (y > max)
        {
            max = y;
        }
        if (z > max)
        {
            max = z;
        }
        return max;
    }

    // 2.7 Двойная сумма
    public int sum2(int x, int y)
    {
        int sum = x + y;
        if (sum >= 10 && sum <= 19)
        {
            return 20;
        }
        return sum;
    }

    // 2.9 День недели (switch)
    public string day(int x)
    {
        switch (x)
        {
            case 1: return "понедельник";
            case 2: return "вторник";
            case 3: return "среда";
            case 4: return "четверг";
            case 5: return "пятница";
            case 6: return "суббота";
            case 7: return "воскресенье";
            default: return "это не день недели";
        }
    }

    // ======================= ЗАДАНИЕ 3. ЦИКЛЫ =======================

    // 3.1 Числа подряд
    public string listNums(int x)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= x; i++)
        {
            if (i > 0)
            {
                sb.Append(' ');
            }
            sb.Append(i);
        }
        return sb.ToString();
    }

    // 3.3 Чётные числа (без if)
    public string chet(int x)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= x; i += 2)
        {
            sb.Append(i).Append(' ');
        }
        return sb.ToString().TrimEnd();
    }

    // 3.5 Длина числа
    public int numLen(long x)
    {
        int count = 0;
        do
        {
            count++;
            x /= 10;
        } while (x != 0);
        return count;
    }

    // 3.7 Квадрат
    public void square(int x)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Console.Write('*');
            }
            Console.WriteLine();
        }
    }

    // 3.9 Правый треугольник
    public void rightTriangle(int x)
    {
        for (int i = 1; i <= x; i++)
        {
            for (int s = 0; s < x - i; s++)
            {
                Console.Write(' ');
            }
            for (int j = 0; j < i; j++)
            {
                Console.Write('*');
            }
            Console.WriteLine();
        }
    }

    // ======================= ЗАДАНИЕ 4. МАССИВЫ =======================

    // 4.1 Поиск первого значения
    public int findFirst(int[] arr, int x)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == x)
            {
                return i;
            }
        }
        return -1;
    }

    // 4.3 Поиск максимального по модулю
    public int maxAbs(int[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            throw new ArgumentException("Массив не должен быть пустым");
        }
        int best = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            // long, чтобы Math.Abs не падал на int.MinValue
            if (Math.Abs((long)arr[i]) > Math.Abs((long)best))
            {
                best = arr[i];
            }
        }
        return best;
    }

    // 4.5 Добавление массива в массив
    public int[] add(int[] arr, int[] ins, int pos)
    {
        if (pos < 0 || pos > arr.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(pos), "Позиция вне допустимого диапазона");
        }
        int[] result = new int[arr.Length + ins.Length];
        for (int i = 0; i < pos; i++)
        {
            result[i] = arr[i];
        }
        for (int i = 0; i < ins.Length; i++)
        {
            result[pos + i] = ins[i];
        }
        for (int i = pos; i < arr.Length; i++)
        {
            result[i + ins.Length] = arr[i];
        }
        return result;
    }

    // 4.7 Возвратный реверс
    public int[] reverseBack(int[] arr)
    {
        int[] result = new int[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            result[i] = arr[arr.Length - 1 - i];
        }
        return result;
    }

    // 4.9 Все вхождения
    public int[] findAll(int[] arr, int x)
    {
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == x)
            {
                count++;
            }
        }
        int[] result = new int[count];
        int k = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == x)
            {
                result[k++] = i;
            }
        }
        return result;
    }

    // ======================= ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ВВОДА =======================

    public int readInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();
            if (int.TryParse(s, out int value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine("Ошибка ввода. Введите целое число" +
                (min != int.MinValue || max != int.MaxValue ? $" от {min} до {max}" : "") + ".");
        }
    }

    public long readLong(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (long.TryParse(Console.ReadLine(), out long value))
            {
                return value;
            }
            Console.WriteLine("Ошибка ввода. Введите целое число.");
        }
    }

    public double readDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                return value;
            }
            Console.WriteLine("Ошибка ввода. Введите вещественное число (например, 5,25).");
        }
    }

    public char readDigitChar(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();
            if (s != null && s.Length == 1 && s[0] >= '0' && s[0] <= '9')
            {
                return s[0];
            }
            Console.WriteLine("Ошибка ввода. Введите один символ от '0' до '9'.");
        }
    }

    // Ввод массива: числа через пробел или запятую
    public int[] readArray(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string line = Console.ReadLine() ?? "";
            string[] parts = line.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                Console.WriteLine("Массив не должен быть пустым.");
                continue;
            }
            int[] arr = new int[parts.Length];
            bool ok = true;
            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i], out arr[i]))
                {
                    ok = false;
                    break;
                }
            }
            if (ok)
            {
                return arr;
            }
            Console.WriteLine("Ошибка ввода. Вводите только целые числа через пробел.");
        }
    }

    public string arrToString(int[] arr)
    {
        return "[" + string.Join(", ", arr) + "]";
    }

    public void printBool(bool b)
    {
        Console.WriteLine("Результат: " + (b ? "true" : "false"));
    }

    // ======================= МЕНЮ ЗАДАНИЙ =======================

    public void runTask1()
    {
        Console.WriteLine("\nЗадание 1. Методы: 1-Дробная часть, 3-Букву в число, 5-Двузначное, 7-Диапазон, 9-Равенство");
        switch (readInt("Номер задачи: ", 1, 9))
        {
            case 1:
                Console.WriteLine("Результат: " + fraction(readDouble("Введите x: ")));
                break;
            case 3:
                Console.WriteLine("Результат: " + charToNum(readDigitChar("Введите цифру-символ: ")));
                break;
            case 5:
                printBool(is2Digits(readInt("Введите x: ")));
                break;
            case 7:
                int a = readInt("Введите a: ");
                int b = readInt("Введите b: ");
                int num = readInt("Введите num: ");
                printBool(isInRange(a, b, num));
                break;
            case 9:
                int p = readInt("Введите a: ");
                int q = readInt("Введите b: ");
                int r = readInt("Введите c: ");
                printBool(isEqual(p, q, r));
                break;
            default:
                Console.WriteLine("Эта задача не входит в нечётный набор.");
                break;
        }
    }

    public void runTask2()
    {
        Console.WriteLine("\nЗадание 2. Условия: 1-Модуль, 3-Тридцать пять, 5-Тройной максимум, 7-Двойная сумма, 9-День недели");
        switch (readInt("Номер задачи: ", 1, 9))
        {
            case 1:
                Console.WriteLine("Результат: " + abs(readInt("Введите x (не int.MinValue): ", int.MinValue + 1)));
                break;
            case 3:
                printBool(is35(readInt("Введите x: ")));
                break;
            case 5:
                int x = readInt("Введите x: ");
                int y = readInt("Введите y: ");
                int z = readInt("Введите z: ");
                Console.WriteLine("Результат: " + max3(x, y, z));
                break;
            case 7:
                int s1 = readInt("Введите x: ", int.MinValue / 2, int.MaxValue / 2);
                int s2 = readInt("Введите y: ", int.MinValue / 2, int.MaxValue / 2);
                Console.WriteLine("Результат: " + sum2(s1, s2));
                break;
            case 9:
                Console.WriteLine("Результат: " + day(readInt("Введите номер дня недели: ")));
                break;
            default:
                Console.WriteLine("Эта задача не входит в нечётный набор.");
                break;
        }
    }

    public void runTask3()
    {
        Console.WriteLine("\nЗадание 3. Циклы: 1-Числа подряд, 3-Чётные числа, 5-Длина числа, 7-Квадрат, 9-Правый треугольник");
        switch (readInt("Номер задачи: ", 1, 9))
        {
            case 1:
                Console.WriteLine("Результат: " + listNums(readInt("Введите x (>= 0): ", 0, 100000)));
                break;
            case 3:
                Console.WriteLine("Результат: " + chet(readInt("Введите x (>= 0): ", 0, 100000)));
                break;
            case 5:
                Console.WriteLine("Результат: " + numLen(readLong("Введите x: ")));
                break;
            case 7:
                square(readInt("Введите размер x (1..50): ", 1, 50));
                break;
            case 9:
                rightTriangle(readInt("Введите высоту x (1..50): ", 1, 50));
                break;
            default:
                Console.WriteLine("Эта задача не входит в нечётный набор.");
                break;
        }
    }

    public void runTask4()
    {
        Console.WriteLine("\nЗадание 4. Массивы: 1-Первое вхождение, 3-Максимум по модулю, 5-Вставка массива, 7-Возвратный реверс, 9-Все вхождения");
        switch (readInt("Номер задачи: ", 1, 9))
        {
            case 1:
                int[] a1 = readArray("Введите массив (через пробел): ");
                Console.WriteLine("Результат: " + findFirst(a1, readInt("Введите x: ")));
                break;
            case 3:
                Console.WriteLine("Результат: " + maxAbs(readArray("Введите массив (через пробел): ")));
                break;
            case 5:
                int[] arr = readArray("Введите массив arr: ");
                int[] ins = readArray("Введите вставляемый массив ins: ");
                int pos = readInt("Введите позицию pos (0.." + arr.Length + "): ", 0, arr.Length);
                Console.WriteLine("Результат: " + arrToString(add(arr, ins, pos)));
                break;
            case 7:
                Console.WriteLine("Результат: " + arrToString(reverseBack(readArray("Введите массив (через пробел): "))));
                break;
            case 9:
                int[] a9 = readArray("Введите массив (через пробел): ");
                Console.WriteLine("Результат: " + arrToString(findAll(a9, readInt("Введите x: "))));
                break;
            default:
                Console.WriteLine("Эта задача не входит в нечётный набор.");
                break;
        }
    }
}