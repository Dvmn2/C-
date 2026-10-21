using System.Globalization;

namespace Lab2;

/// <summary>
/// Точка на двумерной плоскости (задачи 1.1 / 4.1).
/// </summary>
public class Point
{
    private double _x;
    private double _y;

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X
    {
        get { return _x; }
        set { _x = value; }
    }

    public double Y
    {
        get { return _y; }
        set { _y = value; }
    }

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
