namespace Lab2;

/// <summary>
/// Ломаная линия (задачи 3.2, 4.9, 5.7).
/// </summary>
public class Polyline
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Polyline"/> class.
    /// Без параметров создаётся пустая ломаная.
    /// </summary>
    public Polyline(params Point[] points)
    {
        ArgumentNullException.ThrowIfNull(points);

        // Копируем массив, но не сами точки: точки могут быть общими у нескольких ломаных.
        Points = (Point[])points.Clone();
    }

    public Point[] Points { get; private set; }

    /// <summary>
    /// Добавляет точки; создаётся новый массив из старых и новых значений.
    /// </summary>
    public void AddPoints(params Point[] points)
    {
        ArgumentNullException.ThrowIfNull(points);

        Points = Points.Concat(points).ToArray();
    }

    /// <summary>
    /// Общая длина ломаной.
    /// </summary>
    public double GetLength()
    {
        double length = 0;
        for (int i = 1; i < Points.Length; i++)
        {
            length += Points[i - 1].DistanceTo(Points[i]);
        }

        return length;
    }

    public override string ToString()
    {
        return $"Линия [{string.Join(", ", Points.Select(point => point.ToString()))}]";
    }
}
