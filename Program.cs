using System.Text;

namespace Lab4Part1
{
    internal static class Program
    {
        private const string GasolineFilePath = "gasoline.txt";

        private static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("1 - List: убрать повторы подряд идущих элементов");
                Console.WriteLine("2 - LinkedList: двунаправленный список по однонаправленному");
                Console.WriteLine("3 - HashSet: факультативы");
                Console.WriteLine("4 - HashSet: символы слов с нечётными номерами (файл)");
                Console.WriteLine("5 - Dictionary: АЗС с минимальной ценой бензина");
                Console.WriteLine("0 - выход");

                int choice = ConsoleInput.ReadInt("Выберите задание: ", 0, 5);

                try
                {
                    switch (choice)
                    {
                        case 1:
                            RunTask1();
                            break;
                        case 2:
                            RunTask2();
                            break;
                        case 3:
                            RunTask3();
                            break;
                        case 4:
                            RunTask4();
                            break;
                        case 5:
                            RunTask5();
                            break;
                        default:
                            isRunning = false;
                            break;
                    }
                }
                catch (EndOfStreamException)
                {
                    isRunning = false;
                }
                catch (IOException exception)
                {
                    Console.WriteLine("Ошибка работы с файлом: " + exception.Message);
                }
            }
        }

        private static void RunTask1()
        {
            List<int> list = ConsoleInput.ReadIntList("Введите элементы списка через пробел: ");
            CollectionTasks.RemoveConsecutiveDuplicates(list);
            Console.WriteLine("Результат: " + string.Join(" ", list));
        }

        private static void RunTask2()
        {
            List<int> values = ConsoleInput.ReadIntList("Введите элементы однонаправленного списка через пробел: ");

            SinglyLinkedNode head = null;
            SinglyLinkedNode tail = null;
            foreach (int value in values)
            {
                SinglyLinkedNode node = new SinglyLinkedNode(value);
                if (head == null)
                {
                    head = node;
                }
                else
                {
                    tail.Next = node;
                }

                tail = node;
            }

            LinkedList<int> result = CollectionTasks.BuildDoublyLinkedList(head);

            Console.WriteLine("Обход слева направо: " + string.Join(" ", result));

            StringBuilder backward = new StringBuilder();
            for (LinkedListNode<int> node = result.Last; node != null; node = node.Previous)
            {
                backward.Append(node.Value).Append(' ');
            }

            Console.WriteLine("Обход справа налево: " + backward.ToString().TrimEnd());
        }

        private static void RunTask3()
        {
            int electivesCount = ConsoleInput.ReadInt("Количество факультативов в перечне: ", 1, 1000);
            HashSet<string> electives = new HashSet<string>();
            while (electives.Count < electivesCount)
            {
                string name = ConsoleInput.ReadNonEmptyString($"Факультатив {electives.Count + 1}: ");
                if (!electives.Add(name))
                {
                    Console.WriteLine("Ошибка: такой факультатив уже есть в перечне.");
                }
            }

            int studentsCount = ConsoleInput.ReadInt("Количество студентов в группе: ", 1, 1000);
            List<HashSet<string>> students = new List<HashSet<string>>();
            for (int i = 1; i <= studentsCount; i++)
            {
                students.Add(ReadStudentElectives(i, electives));
            }

            PrintSet("Ходят все студенты", CollectionTasks.GetElectivesForAll(electives, students));
            PrintSet("Ходит хотя бы один студент", CollectionTasks.GetElectivesForAny(electives, students));
            PrintSet("Не ходит ни один студент", CollectionTasks.GetElectivesForNone(electives, students));
        }

        private static HashSet<string> ReadStudentElectives(int number, HashSet<string> electives)
        {
            while (true)
            {
                string line = ConsoleInput.ReadLine(
                    $"Студент {number}: факультативы через запятую (пустая строка - никакие): ");
                HashSet<string> result = new HashSet<string>();
                bool isValid = true;

                if (line.Trim().Length > 0)
                {
                    string[] parts = line.Split(',');
                    for (int i = 0; i < parts.Length && isValid; i++)
                    {
                        string name = parts[i].Trim();
                        if (electives.Contains(name))
                        {
                            result.Add(name);
                        }
                        else
                        {
                            Console.WriteLine($"Ошибка: факультатива \"{name}\" нет в перечне.");
                            isValid = false;
                        }
                    }
                }

                if (isValid)
                {
                    return result;
                }
            }
        }

        private static void PrintSet(string title, HashSet<string> set)
        {
            Console.WriteLine(title + ": " + (set.Count == 0 ? "нет" : string.Join(", ", set)));
        }

        private static void RunTask4()
        {
            string path;
            while (true)
            {
                path = ConsoleInput.ReadNonEmptyString("Введите путь к текстовому файлу: ");
                if (File.Exists(path))
                {
                    break;
                }

                Console.WriteLine("Ошибка: файл не найден.");
            }

            List<string> symbols = CollectionTasks.GetSymbolsFromOddWords(path);
            Console.WriteLine(symbols.Count == 0 ? "Символов нет." : string.Join(" ", symbols));
        }

        private static void RunTask5()
        {
            CollectionTasks.FillGasolineFile(GasolineFilePath);

            try
            {
                int[] counts = CollectionTasks.CountCheapestStations(GasolineFilePath);
                Console.WriteLine("Результат (92 95 98): " + string.Join(" ", counts));
            }
            catch (InvalidDataException exception)
            {
                Console.WriteLine("Ошибка данных: " + exception.Message);
            }
        }
    }
}
