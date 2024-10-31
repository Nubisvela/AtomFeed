namespace AtomFeed.Element;

/// <summary>
/// Contains metadata from the source feed if the entry is a copy.
/// </summary>
public class Source
{
    /// <summary>
    /// <c>Required</c>
    /// Identifies the source using a universally unique and permanent URI.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// <c>Required</c>
    /// A human-readable title for the source.
    /// </summary>
    public required Text Title { get; set; }

    /// <summary>
    /// <c>Required</c>
    /// The last time the source was modified.
    /// </summary>
    public required DateTimeOffset Updated { get; set; }
}
