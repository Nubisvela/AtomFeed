namespace AtomFeed.Element;

/// <summary>
/// Atom feed root element.
/// </summary>
public class Feed
{
    /// <summary>
    /// <c>Required</c>
    /// Identifies the feed using a universally unique and permanent URI. If you have a long-term,
    /// renewable lease on your Internet domain name, then you can feel free to use your website's address.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// <c>Required</c>
    /// A human-readable title for the feed.
    /// </summary>
    public required Text Title { get; set; }

    /// <summary>
    /// <c>Required</c>
    /// The last time the feed was modified.
    /// </summary>
    public required DateTimeOffset Updated { get; set; }

    /// <summary>
    /// <c>Required</c>
    /// Entry list of the feed. An entry would be a single post on a weblog.
    /// </summary>
    public List<Entry> Entries { get; set; } = [];

    /// <summary>
    /// <c>Recommended</c>
    /// Names one author of the feed. A feed may have multiple authors.
    /// A feed must contain at least one author unless all the entries
    /// contain at least one author element.
    /// </summary>
    public List<Author> Authors { get; set; } = [];

    /// <summary>
    /// <c>Recommended</c>
    /// Identifies a related Web page. The type of relation is defined by the rel attribute.
    /// A feed is limited to one alternate per type and hreflang.
    /// A feed should contain a link back to the feed itself.
    /// </summary>
    public List<Link> Links { get; set; } = [];

    /// <summary>
    /// <c>Optional</c>
    /// Specifies a category that the feed belongs to. A feed may have multiple categories.
    /// </summary>
    public List<Category> Categories { get; set; } = [];

    /// <summary>
    /// <c>Optional</c>
    /// Names one contributor to the feed. A feed may have multiple contributors.
    /// </summary>
    public List<Contributor> Contributors { get; set; } = [];

    /// <summary>
    /// <c>Optional</c>
    /// Identifies the software used to generate the feed, for debugging and other purposes.
    /// </summary>
    public Generator? Generator { get; set; }

    /// <summary>
    /// <c>Optional</c>
    /// Identifies a small image which provides iconic visual identification for the feed.
    /// Icons should be square.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <c>Optional</c>
    /// Identifies a larger image which provides visual identification for the feed.
    /// Images should be twice as wide as they are tall.
    /// </summary>
    public string? Logo { get; set; }

    /// <summary>
    /// <c>Optional</c>
    /// Conveys information about rights, e.g. copyrights, held in and over the feed.
    /// </summary>
    public Text? Rights { get; set; }

    /// <summary>
    /// <c>Optional</c>
    /// Contains a human-readable description or subtitle for the feed.
    /// </summary>
    public Text? Subtitle { get; set; }
}
