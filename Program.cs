using System.Globalization;
using System.Text;

namespace Lab4Part2
{
    internal static class Program
    {
        private static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== Конструкторы ===");
            Triangle defaultTriangle = new Triangle();
            Console.WriteLine("Без параметров: " + defaultTriangle);

            Console.WriteLine("Введите стороны первого треугольника.");
            Triangle first = new Triangle(ReadSide("a"), ReadSide("b"), ReadSide("c"));
            Console.WriteLine("С параметрами: " + first);

            Triangle copy = new Triangle(first);
            Console.WriteLine("Копирования: " + copy);

            Console.WriteLine();
            Console.WriteLine("=== Свойства (проверка значения) ===");
            TestSetter(first);

            Console.WriteLine();
            Console.WriteLine("Введите стороны второго треугольника.");
            Triangle second = new Triangle(ReadSide("a"), ReadSide("b"), ReadSide("c"));

            Console.WriteLine();
            Console.WriteLine("=== Методы и операции ===");
            PrintInfo("Первый", first);
            PrintInfo("Второй", second);
            Console.WriteLine($"Первый < второй (по площади): {first < second}");
            Console.WriteLine($"Первый > второй (по площади): {first > second}");
        }

        private static void PrintInfo(string title, Triangle triangle)
        {
            double perimeter = triangle;
            Console.WriteLine($"{title}: {triangle}");
            Console.WriteLine($"  Exists(): {triangle.Exists()}");
            Console.WriteLine($"  Площадь (-t): {(-triangle).ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"  Периметр ((double)t): {perimeter.ToString(CultureInfo.InvariantCulture)}");
            Console.WriteLine($"  Существует ((bool)t): {(bool)triangle}");
        }

        private static void TestSetter(Triangle triangle)
        {
            while (true)
            {
                double value = ReadDouble("Новое значение стороны a: ");
                try
                {
                    triangle.A = value;
                    Console.WriteLine("Принято: " + triangle);
                    return;
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    Console.WriteLine("Отклонено: " + exception.Message);
                }
            }
        }

        private static double ReadSide(string name)
        {
            while (true)
            {
                double value = ReadDouble($"Сторона {name}: ");
                if (value > 0.0)
                {
                    return value;
                }

                Console.WriteLine("Ошибка: длина стороны должна быть положительной.");
            }
        }

        private static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string line = Console.ReadLine();
                if (line == null)
                {
                    throw new InvalidOperationException("Ввод данных завершён.");
                }

                line = line.Trim().Replace(',', '.');
                if (double.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                    && !double.IsNaN(value) && !double.IsInfinity(value))
                {
                    return value;
                }

                Console.WriteLine("Ошибка: введите вещественное число.");
            }
        }
    }
}
