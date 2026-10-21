using System.Text;

namespace Lab2;

/// <summary>
/// Имя: фамилия, личное имя, отчество (задача 1.3).
/// Любой из параметров может быть не задан.
/// </summary>
public class Name
{
    private string? _lastName;
    private string? _firstName;
    private string? _middleName;

    public Name(string? lastName = null, string? firstName = null, string? middleName = null)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }

    public string? LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public string? FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public string? MiddleName
    {
        get { return _middleName; }
        set { _middleName = value; }
    }

    /// <summary>
    /// Формат: «Фамилия Имя Отчество», незаданные части пропускаются.
    /// </summary>
    public override string ToString()
    {
        string?[] parts = { LastName, FirstName, MiddleName };
        var result = new StringBuilder();
        foreach (string? part in parts)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                continue;
            }

            if (result.Length > 0)
            {
                result.Append(' ');
            }

            result.Append(part);
        }

        return result.ToString();
    }
}
