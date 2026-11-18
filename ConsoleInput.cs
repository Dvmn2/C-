using System.Globalization;

namespace Lab4Part1
{
    /// <summary>
    /// Вспомогательные методы ввода данных с клавиатуры с проверкой корректности.
    /// </summary>
    internal static class ConsoleInput
    {
        public static string ReadLine(string prompt)
        {
            Console.Write(prompt);
            string line = Console.ReadLine();
            if (line == null)
            {
                throw new EndOfStreamException("Ввод данных завершён.");
            }

            return line;
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                string line = ReadLine(prompt);
                if (int.TryParse(line, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value)
                    && value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine($"Ошибка: введите целое число от {min} до {max}.");
            }
        }

        public static string ReadNonEmptyString(string prompt)
        {
            while (true)
            {
                string line = ReadLine(prompt).Trim();
                if (line.Length > 0)
                {
                    return line;
                }

                Console.WriteLine("Ошибка: строка не должна быть пустой.");
            }
        }

        public static List<int> ReadIntList(string prompt)
        {
            while (true)
            {
                string line = ReadLine(prompt);
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> result = new List<int>();
                bool isValid = parts.Length > 0;

                for (int i = 0; i < parts.Length && isValid; i++)
                {
                    if (int.TryParse(parts[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
                    {
                        result.Add(value);
                    }
                    else
                    {
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    return result;
                }

                Console.WriteLine("Ошибка: введите хотя бы одно целое число; числа разделяйте пробелами.");
            }
        }
    }
}
