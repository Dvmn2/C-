namespace Lab2;

/// <summary>
/// Имя: фамилия, личное имя, отчество (задача 1.3).
/// Любой из параметров может быть не задан.
/// </summary>
public class Name
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Name"/> class.
    /// </summary>
    public Name(string? lastName = null, string? firstName = null, string? middleName = null)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    /// <summary>
    /// Формат: «Фамилия Имя Отчество», незаданные части пропускаются.
    /// </summary>
    public override string ToString()
    {
        string?[] parts = { LastName, FirstName, MiddleName };
        return string.Join(" ", parts.Where(part => !string.IsNullOrWhiteSpace(part)));
    }
}
