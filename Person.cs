namespace Lab2;

/// <summary>
/// Человек (задачи 2.2 и 2.3): имя в виде объекта <see cref="Name"/>, рост и необязательный отец.
/// </summary>
public class Person
{
    private Name _name;
    private Person? _father;
    private int _height;

    public Person(Name name, int height, Person? father = null)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        Height = height;
        Father = father;
    }

    public Name Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Height
    {
        get { return _height; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Рост должен быть положительным.");
            }
            if (value >= 300)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Рост должен быть меньше 3-х метров.");
            }

            _height = value;
        }
    }

    public Person? Father
    {
        get { return _father; }
        set
        {
            for (Person? ancestor = value; ancestor != null; ancestor = ancestor._father)
            {
                if (ReferenceEquals(ancestor, this))
                {
                    throw new ArgumentException("Человек не может быть собственным предком.");
                }
            }

            _father = value;
        }
    }

    /// <summary>
    /// Имя с учётом отца: недостающие фамилия и отчество берутся от отца.
    /// Исходный объект <see cref="Name"/> при этом не изменяется.
    /// </summary>
    public Name GetFullName()
    {
        Name? fatherName = Father?.GetFullName();

        string? lastName = Name.LastName;
        if (string.IsNullOrWhiteSpace(lastName))
        {
            lastName = fatherName?.LastName;
        }

        string? middleName = Name.MiddleName;
        if (string.IsNullOrWhiteSpace(middleName) && !string.IsNullOrWhiteSpace(fatherName?.FirstName))
        {
            middleName = CreatePatronymic(fatherName.FirstName);
        }

        return new Name(lastName, Name.FirstName, middleName);
    }

    public override string ToString()
    {
        return $"{GetFullName()}, рост: {Height}";
    }

    /// <summary>
    /// Образует отчество по имени отца (мужской род).
    /// </summary>
    private static string CreatePatronymic(string fatherFirstName)
    {
        string name = fatherFirstName.Trim();
        char last = char.ToLowerInvariant(name[^1]);

        switch (last)
        {
            case 'й':
            case 'ь':
                return name[..^1] + "евич";
            case 'а':
            case 'я':
                return name[..^1] + "ич";
            case 'о':
            case 'е':
            case 'и':
            case 'у':
            case 'ы':
            case 'э':
            case 'ю':
                return name + "вич";
            default:
                return name + "ович";
        }
    }
}
