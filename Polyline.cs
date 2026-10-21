namespace Lab2;

/// <summary>
/// Ломаная линия (задачи 3.2, 4.9, 5.7).
/// </summary>
public class Polyline
{
    private Point[] _points;

    public Polyline(params Point[] points)
    {
        ArgumentNullException.ThrowIfNull(points);

        _points = (Point[])points.Clone();
    }

    public Point[] Points
    {
        get { return _points; }
        private set { _points = value; }
    }

    /// <summary>
    /// Добавляет точки; создаётся новый массив из старых и новых значений.
    /// </summary>
    public void AddPoints(params Point[] points)
    {
        ArgumentNullException.ThrowIfNull(points);

        var combined = new Point[Points.Length + points.Length];
        Array.Copy(Points, 0, combined, 0, Points.Length);
        Array.Copy(points, 0, combined, Points.Length, points.Length);

        Points = combined;
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
        var texts = new string[Points.Length];
        for (int i = 0; i < Points.Length; i++)
        {
            texts[i] = Points[i].ToString();
        }

        return $"Линия [{string.Join(", ", texts)}]";
    }
}
