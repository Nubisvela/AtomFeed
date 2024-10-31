namespace AtomFeed.Element;

/// <summary>
/// Link is patterned after html's link element.
/// </summary>
public class Link
{
    /// <summary>
    /// The URI of the referenced resource (typically a Web page).
    /// </summary>
    public required string Href { get; set; }

    /// <summary>
    /// A single link relationship type.
    /// <para></para>
    /// It can be a full URI, or one of the following predefined values (default=<c>alternate</c>):
    /// <list type="bullet">
    /// <item><term><c>alternate</c>:</term>
    /// <description>an alternate representation of the entry or feed, for example a permalink to the
    /// html version of the entry, or the front page of the weblog.</description></item>
    /// <item><term><c>enclosure</c>:</term>
    /// <description>a related resource which is potentially large and might require special
    /// handling, for example an audio or video recording.</description></item>
    /// <item><term><c>related</c>:</term>
    /// <description>a document related to the entry or feed.</description></item>
    /// <item><term><c>self</c>:</term>
    /// <description>the feed itself.</description></item>
    /// <item><term><c>via</c>:</term>
    /// <description>the source of the information provided in the entry.</description></item>
    /// </list>
    /// </summary>
    public string? Relation { get; set; }

    /// <summary>
    /// Indicates the media type of the resource.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Indicates the language of the referenced resource.
    /// </summary>
    public string? HrefLanguage { get; set; }

    /// <summary>
    /// Human-readable information about the link, typically for display purposes.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The length of the resource, in bytes.
    /// </summary>
    public long? Length { get; set; }

    public static implicit operator Link(string href)
    {
        return new Link
        {
            Href = href
        };
    }

    public override string ToString() => Href;
}
