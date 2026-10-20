using System.Globalization;

namespace Lab2;

/// <summary>
/// Точка на двумерной плоскости (задачи 1.1 / 4.1).
/// </summary>
public class Point
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Point"/> class.
    /// Создать точку можно только указав обе координаты.
    /// </summary>
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; set; }

    public double Y { get; set; }

    /// <summary>
    /// Расстояние до другой точки.
    /// </summary>
    public double DistanceTo(Point other)
    {
        ArgumentNullException.ThrowIfNull(other);

        double dx = X - other.X;
        double dy = Y - other.Y;
        return Math.Sqrt((dx * dx) + (dy * dy));
    }

    public override string ToString()
    {
        return string.Create(CultureInfo.InvariantCulture, $"{{{X};{Y}}}");
    }
}
