using System;
using System.Globalization;
using System.Text;

namespace Lab3;

/// <summary>
/// Двумерный массив (матрица). Задания 1-3, вариант 10.
/// Единственное поле класса - двумерный массив.
/// </summary>
public class Matrix
{
    private readonly double[,] _data;

    /// <summary>
    /// Задание 1, массив 1: элементы вводятся с клавиатуры по столбцам,
    /// от первого элемента столбца к последнему.
    /// </summary>
    public Matrix(int rows, int columns)
    {
        CheckSize(rows, columns);
        _data = new double[rows, columns];

        Console.WriteLine("Ввод по столбцам, от первого элемента столбца к последнему.");
        for (int j = 0; j < columns; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                _data[i, j] = ConsoleInput.ReadDouble($"Элемент [{i + 1}, {j + 1}] = ", -1000000, 1000000);
            }
        }
    }

    /// <summary>
    /// Задание 1, массив 2: нечетные числа записываются в "черные" клетки шахматной доски,
    /// четные - в "белые". Клетка [0, 0] считается черной (как a1 на шахматной доске).
    /// </summary>
    public Matrix(int size, Random random)
    {
        CheckSize(size, size);
        _data = new double[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                bool isBlack = (i + j) % 2 == 0;
                int half = random.Next(0, 50);
                _data[i, j] = isBlack ? 2 * half + 1 : 2 * half + 2;
            }
        }
    }

    /// <summary>
    /// Задание 1, массив 3: заполнение числами 1, 2, 3, ... по диагоналям, параллельным побочной,
    /// начиная с правого нижнего угла; направление обхода диагоналей чередуется (змейкой).
    /// </summary>
    public Matrix(int size)
    {
        CheckSize(size, size);
        _data = new double[size, size];

        int value = 1;
        int diagonalCount = 2 * size - 1;

        for (int k = 0; k < diagonalCount; k++)
        {
            int sum = 2 * size - 2 - k;
            int firstRow = Math.Max(0, sum - (size - 1));
            int lastRow = Math.Min(size - 1, sum);

            if (k % 2 == 0)
            {
                for (int i = firstRow; i <= lastRow; i++)
                {
                    _data[i, sum - i] = value;
                    value++;
                }
            }
            else
            {
                for (int i = lastRow; i >= firstRow; i--)
                {
                    _data[i, sum - i] = value;
                    value++;
                }
            }
        }
    }

    private Matrix(double[,] data)
    {
        _data = data;
    }

    public int Rows
    {
        get { return _data.GetLength(0); }
    }

    public int Columns
    {
        get { return _data.GetLength(1); }
    }

    /// <summary>
    /// Задание 2: количество элементов, больших любого элемента главной диагонали
    /// (то есть больших наибольшего элемента главной диагонали).
    /// </summary>
    public int CountGreaterThanMainDiagonal()
    {
        int diagonalLength = Math.Min(Rows, Columns);
        double maxDiagonal = _data[0, 0];

        for (int k = 1; k < diagonalLength; k++)
        {
            if (_data[k, k] > maxDiagonal)
            {
                maxDiagonal = _data[k, k];
            }
        }

        int count = 0;
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (_data[i, j] > maxDiagonal)
                {
                    count++;
                }
            }
        }

        return count;
    }

    /// <summary>
    /// Транспонирование матрицы.
    /// </summary>
    public Matrix Transpose()
    {
        double[,] result = new double[Columns, Rows];

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                result[j, i] = _data[i, j];
            }
        }

        return new Matrix(result);
    }

    public static Matrix operator +(Matrix left, Matrix right)
    {
        if (left is null || right is null)
        {
            throw new ArgumentNullException(left is null ? nameof(left) : nameof(right));
        }

        if (left.Rows != right.Rows || left.Columns != right.Columns)
        {
            throw new ArgumentException("Для сложения матрицы должны иметь одинаковые размеры.");
        }

        double[,] result = new double[left.Rows, left.Columns];

        for (int i = 0; i < left.Rows; i++)
        {
            for (int j = 0; j < left.Columns; j++)
            {
                result[i, j] = left._data[i, j] + right._data[i, j];
            }
        }

        return new Matrix(result);
    }

    public static Matrix operator *(Matrix left, Matrix right)
    {
        if (left is null || right is null)
        {
            throw new ArgumentNullException(left is null ? nameof(left) : nameof(right));
        }

        if (left.Columns != right.Rows)
        {
            throw new ArgumentException(
                "Для умножения число столбцов первой матрицы должно равняться числу строк второй.");
        }

        double[,] result = new double[left.Rows, right.Columns];

        for (int i = 0; i < left.Rows; i++)
        {
            for (int j = 0; j < right.Columns; j++)
            {
                double sum = 0;
                for (int k = 0; k < left.Columns; k++)
                {
                    sum += left._data[i, k] * right._data[k, j];
                }

                result[i, j] = sum;
            }
        }

        return new Matrix(result);
    }

    /// <summary>
    /// Формирует строку для вывода матрицы на экран в виде таблицы.
    /// </summary>
    public override string ToString()
    {
        string[,] cells = new string[Rows, Columns];
        int width = 0;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                cells[i, j] = _data[i, j].ToString("0.##", CultureInfo.InvariantCulture);
                if (cells[i, j].Length > width)
                {
                    width = cells[i, j].Length;
                }
            }
        }

        width += 2;
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                builder.Append(cells[i, j].PadLeft(width));
            }

            builder.AppendLine();
        }

        return builder.ToString();
    }

    private static void CheckSize(int rows, int columns)
    {
        if (rows <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rows), "Число строк должно быть положительным.");
        }

        if (columns <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(columns), "Число столбцов должно быть положительным.");
        }
    }
}
