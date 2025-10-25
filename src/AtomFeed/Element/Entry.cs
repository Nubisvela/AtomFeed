namespace AtomFeed.Element;

/// <summary>
/// An entry would be a single post on a weblog.
/// </summary>
public class Entry {
    /// <summary>
    /// <c>Required</c>
    /// Identifies the entry using a universally unique and permanent URI.
    /// Two entries in a feed can have the same value for id if they
    /// represent the same entry at different points in time.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// <c>Required</c>
    /// Contains a human-readable title for the entry. This value should not be blank.
    /// </summary>
    public required Text Title { get; set; }

    /// <summary>
    /// <c>Required</c>
    /// The last time the entry was modified.
    /// This value need not change after a typo is fixed, only after a substantial modification.
    /// Generally, different entries in a feed will have different updated timestamps.
    /// </summary>
    public required DateTimeOffset Updated { get; set; }

    /// <summary>
    /// <c>Recommended</c>
    /// Names one author of the entry. An entry may have multiple authors.
    /// An entry must contain at least one author unless there is an author
    /// in the enclosing feed, or there is an author in the enclosed source.
    /// </summary>
    public List<Author> Authors { get; set; } = [];

    /// <summary>
    /// <c>Recommended</c>
    /// Contains or links to the complete content of the entry. Content must be provided
    /// if there is no alternate link, and should be provided if there is no summary.
    /// </summary>
    public Content? Content { get; set; }

    /// <summary>
    /// <c>Recommended</c>
    /// Identifies related Web pages. The type of relation is defined by the rel attribute.
    /// An entry is limited to one alternate per type and hreflang. An entry must contain an
    /// alternate link if there is no content element.
    /// </summary>
    public List<Link> Links { get; set; } = [];

    /// <summary>
    /// <c>Recommended</c>
    /// Conveys a short summary, abstract, or excerpt of the entry. Summary should be provided
    /// if there either is no content provided for the entry, or that content is not inline
    /// (i.e., contains a src attribute), or if the content is encoded in base64.
    /// </summary>
    public Text? Summary { get; set; }

    /// <summary>
    /// <c>Optional</c>
    /// Specifies categories that the entry belongs to.
    /// </summary>
    public List<Category> Categories { get; set; } = [];

    /// <summary>
    /// <c>Optional</c>
    /// Names contributors to the entry.
    /// </summary>
    public List<Contributor> Contributors { get; set; } = [];

    /// <summary>
    /// <c>Optional</c>
    /// Contains the time of the initial creation or first availability of the entry.
    /// </summary>
    public DateTimeOffset? Published { get; set; }

    /// <summary>
    /// <c>Optional</c>
    /// Conveys information about rights, e.g. copyrights, held in and over the entry.
    /// </summary>
    public Text? Rights { get; set; }

    /// <summary>
    /// <c>Optional</c>
    /// Contains metadata from the source feed if this entry is a copy.
    /// </summary>
    public Source? Source { get; set; }
}