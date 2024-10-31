namespace AtomFeed.Element;

/// <summary>
/// Identifies the software used to generate the feed, for debugging and other purposes.
/// </summary>
public class Generator
{
    /// <summary>
    /// Name of the software.
    /// </summary>
    public required string Value { get; set; } = "AtomFeed";

    /// <summary>
    /// The URI of the source (typically a Web page) used to generate the feed.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// The version of the software.
    /// </summary>
    public string? Version { get; set; } = "1.0.0";

    public static implicit operator Generator(string value)
    {
        return new Generator
        {
            Value = value
        };
    }

    public override string ToString() => Value;
}
