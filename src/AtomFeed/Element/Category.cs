namespace AtomFeed.Element;

/// <summary>
/// Specifies a category that the feed or entry belongs to.
/// </summary>
public class Category
{
    /// <summary>
    /// Identifies the category.
    /// </summary>
    public required string Term { get; set; }

    /// <summary>
    /// Identifies the categorization scheme via a URI.
    /// </summary>
    public string? Scheme { get; set; }

    /// <summary>
    /// Provides a human-readable label for display.
    /// </summary>
    public string? Label { get; set; }

    public static implicit operator Category(string value)
    {
        return new Category
        {
            Term = value
        };
    }

    public override string ToString() => Term;
}
