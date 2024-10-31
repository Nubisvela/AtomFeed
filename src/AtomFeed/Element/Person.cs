namespace AtomFeed.Element;

/// <summary>
/// <inheritdoc cref="Person"/>
/// </summary>
public class Author : Person
{
    public static implicit operator Author(string name)
    {
        return new Author
        {
            Name = name
        };
    }

    public static Author FromPerson(Person person)
    {
        return new Author
        {
            Name = person.Name,
            Email = person.Email,
            Url = person.Url
        };
    }
}

/// <summary>
/// <inheritdoc cref="Person"/>
/// </summary>
public class Contributor : Person
{
    public static implicit operator Contributor(string name)
    {
        return new Contributor
        {
            Name = name
        };
    }

    public static Contributor FromPerson(Person person)
    {
        return new Contributor
        {
            Name = person.Name,
            Email = person.Email,
            Url = person.Url
        };
    }
}

/// <summary>
/// <c>Author</c> and <c>Contributor</c> describe a person, corporation, or similar entity.
/// </summary>
public class Person
{
    /// <summary>
    /// Conveys a human-readable name for the person.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Contains an email address for the person.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Contains a home page for the person.
    /// </summary>
    public string? Url { get; set; }

    public override string ToString() => Name;
}
