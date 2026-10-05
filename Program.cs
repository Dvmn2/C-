using System.Globalization;
using System.Text;

// Лабораторная работа №1. Решены нечётные задачи (1, 3, 5, 7, 9) из каждого задания.
public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Lab1 lab = new Lab1();
        Console.WriteLine("=== Лабораторная работа №1 ===");
        while (true)
        {
            Console.WriteLine("\nВыберите задание: 1-Методы, 2-Условия, 3-Циклы, 4-Массивы, 0-Выход");
            int task = lab.readInt("Ваш выбор: ", 0, 4);
            if (task == 0)
            {
                Console.WriteLine("До свидания!");
                break;
            }
            try
            {
                switch (task)
                {
                    case 1: lab.runTask1(); break;
                    case 2: lab.runTask2(); break;
                    case 3: lab.runTask3(); break;
                    case 4: lab.runTask4(); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}