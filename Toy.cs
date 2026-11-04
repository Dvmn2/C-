using System.Globalization;

namespace Lab3;

/// <summary>
/// Сведения об игрушке: название, стоимость в рублях и возрастные границы.
/// Свойства с явными полями (не автосвойства), чтобы работала XML-сериализация.
/// </summary>
public struct Toy
{
    private string _name;
    private double _price;
    private int _minAge;
    private int _maxAge;

    public Toy(string name, double price, int minAge, int maxAge)
    {
        _name = name;
        _price = price;
        _minAge = minAge;
        _maxAge = maxAge;
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public double Price
    {
        get { return _price; }
        set { _price = value; }
    }

    public int MinAge
    {
        get { return _minAge; }
        set { _minAge = value; }
    }

    public int MaxAge
    {
        get { return _maxAge; }
        set { _maxAge = value; }
    }

    public override string ToString()
    {
        string price = _price.ToString("0.##", CultureInfo.InvariantCulture);
        return $"{_name}, {price} руб., возраст {_minAge}-{_maxAge} лет";
    }
}
