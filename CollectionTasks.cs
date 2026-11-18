using System.Globalization;
using System.Text;

namespace Lab4Part1
{
    /// <summary>
    /// Решения заданий 1-5 лабораторной работы №4 (вариант 10).
    /// </summary>
    public static class CollectionTasks
    {
        private const int MaxNameLength = 20;
        private const int MinPrice = 1000;
        private const int MaxPrice = 3000;
        private const int MaxRecords = 1000000;
        private const int RecordPartsCount = 4;

        private static readonly int[] GasolineGrades = { 92, 95, 98 };

        // ---------- Задание 1. List ----------

        /// <summary>
        /// Оставляет в списке по одному элементу из каждой группы подряд идущих одинаковых элементов.
        /// </summary>
        public static void RemoveConsecutiveDuplicates(List<int> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.Count < 2)
            {
                return;
            }

            int writeIndex = 1;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] != list[writeIndex - 1])
                {
                    list[writeIndex] = list[i];
                    writeIndex++;
                }
            }

            list.RemoveRange(writeIndex, list.Count - writeIndex);
        }

        // ---------- Задание 2. LinkedList ----------

        /// <summary>
        /// Строит двунаправленный список по однонаправленному списку, заданному головным узлом.
        /// </summary>
        public static LinkedList<int> BuildDoublyLinkedList(SinglyLinkedNode head)
        {
            LinkedList<int> result = new LinkedList<int>();
            for (SinglyLinkedNode node = head; node != null; node = node.Next)
            {
                result.AddLast(node.Value);
            }

            return result;
        }

        // ---------- Задание 3. HashSet (факультативы) ----------

        /// <summary>
        /// Факультативы, на которые ходят все студенты.
        /// </summary>
        public static HashSet<string> GetElectivesForAll(
            HashSet<string> electives,
            List<HashSet<string>> students)
        {
            HashSet<string> result = new HashSet<string>(electives);
            foreach (HashSet<string> student in students)
            {
                result.IntersectWith(student);
            }

            return result;
        }

        /// <summary>
        /// Факультативы, на которые ходит хотя бы один студент.
        /// </summary>
        public static HashSet<string> GetElectivesForAny(
            HashSet<string> electives,
            List<HashSet<string>> students)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (HashSet<string> student in students)
            {
                result.UnionWith(student);
            }

            result.IntersectWith(electives);
            return result;
        }

        /// <summary>
        /// Факультативы, на которые не ходит ни один студент.
        /// </summary>
        public static HashSet<string> GetElectivesForNone(
            HashSet<string> electives,
            List<HashSet<string>> students)
        {
            HashSet<string> result = new HashSet<string>(electives);
            result.ExceptWith(GetElectivesForAny(electives, students));
            return result;
        }

        // ---------- Задание 4. HashSet (текстовый файл) ----------

        /// <summary>
        /// Возвращает в алфавитном порядке символы, встречающиеся хотя бы раз в словах
        /// с нечётными номерами (нумерация с 1). Словом считается последовательность букв и цифр.
        /// </summary>
        public static List<string> GetSymbolsFromOddWords(string path)
        {
            HashSet<char> symbols = new HashSet<char>();

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                int wordNumber = 0;
                bool isInWord = false;
                int code;

                while ((code = reader.Read()) != -1)
                {
                    char symbol = (char)code;
                    if (char.IsLetterOrDigit(symbol))
                    {
                        if (!isInWord)
                        {
                            isInWord = true;
                            wordNumber++;
                        }

                        if (wordNumber % 2 == 1)
                        {
                            symbols.Add(char.ToLowerInvariant(symbol));
                        }
                    }
                    else
                    {
                        isInWord = false;
                    }
                }
            }

            List<string> result = new List<string>();
            foreach (char symbol in symbols)
            {
                result.Add(symbol.ToString());
            }

            result.Sort(StringComparer.Create(new CultureInfo("ru-RU"), false));
            return result;
        }

        // ---------- Задание 5. Dictionary (АЗС) ----------

        /// <summary>
        /// Заполняет файл исходными данными, вводимыми с клавиатуры.
        /// Формат: первая строка - N, далее N строк "Компания Улица Марка Цена".
        /// </summary>
        public static void FillGasolineFile(string path)
        {
            int count = ConsoleInput.ReadInt("Введите количество данных N: ", 1, MaxRecords);

            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.WriteLine(count);

                string line;

                for (int i = 1; i <= count; i++)
                {
                    while (true)
                    {
                        line = ConsoleInput.ReadLine($"Запись {i} (Компания Улица Марка Цена): ");
                        if (TryParseRecord(line, out int grade, out int price))
                        {
                            writer.WriteLine(line);
                            break;
                        }

                        Console.WriteLine(
                            $"Ошибка: нужно 4 поля через один пробел; компания и улица - до {MaxNameLength} " +
                            $"символов, марка - 92, 95 или 98, цена - от {MinPrice} до {MaxPrice}.");
                    }
                }
            }
        }

        /// <summary>
        /// Для каждой марки (92, 95, 98) определяет, сколько АЗС продают её дешевле всего.
        /// Один проход по файлу: время O(N), память O(1).
        /// </summary>
        public static int[] CountCheapestStations(string path)
        {
            Dictionary<int, int> minPrices = new Dictionary<int, int>();
            Dictionary<int, int> counts = new Dictionary<int, int>();

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                if (!int.TryParse(reader.ReadLine(), NumberStyles.None, CultureInfo.InvariantCulture, out int count))
                {
                    throw new InvalidDataException("Некорректное количество записей в файле.");
                }

                for (int i = 0; i < count; i++)
                {
                    if (!TryParseRecord(reader.ReadLine(), out int grade, out int price))
                    {
                        throw new InvalidDataException($"Некорректная запись номер {i + 1} в файле.");
                    }

                    if (!minPrices.TryGetValue(grade, out int minPrice) || price < minPrice)
                    {
                        minPrices[grade] = price;
                        counts[grade] = 1;
                    }
                    else if (price == minPrice)
                    {
                        counts[grade]++;
                    }
                }
            }

            int[] result = new int[GasolineGrades.Length];
            for (int i = 0; i < GasolineGrades.Length; i++)
            {
                counts.TryGetValue(GasolineGrades[i], out result[i]);
            }

            return result;
        }

        private static bool TryParseRecord(string line, out int grade, out int price)
        {
            grade = 0;
            price = 0;

            if (line == null)
            {
                return false;
            }

            string[] parts = line.Split(' ');
            if (parts.Length != RecordPartsCount)
            {
                return false;
            }

            for (int i = 0; i < 2; i++)
            {
                if (parts[i].Length == 0 || parts[i].Length > MaxNameLength)
                {
                    return false;
                }
            }

            if (!int.TryParse(parts[2], NumberStyles.None, CultureInfo.InvariantCulture, out grade)
                || Array.IndexOf(GasolineGrades, grade) < 0)
            {
                return false;
            }

            return int.TryParse(parts[3], NumberStyles.None, CultureInfo.InvariantCulture, out price)
                && price >= MinPrice && price <= MaxPrice;
        }
    }
}
