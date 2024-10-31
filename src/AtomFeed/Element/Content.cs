namespace AtomFeed.Element;

/// <summary>
/// Content either contains, or links to, the complete content of the entry.
/// <para></para>
/// <para>In the most common case, the type attribute is either <c>text</c>, <c>html</c>, <c>xhtml</c>,
/// in which case the content element is defined identically to other text constructs.</para>
/// <para>If the <c>Src</c> attribute is present, it represents the URI of where the content
/// can be found. The <c>Type</c> attribute, if present, is the media type of the content.</para>
/// <para>Otherwise, if the <c>Type</c> attribute ends in <c>+xml</c> or <c>/xml</c>, then
/// an XML document of this type is contained in <c>Value</c>.</para>
/// <para>Otherwise, if the <c>Type</c> attribute starts with <c>text</c>, then an escaped document
/// of this type is contained in <c>Value</c>.</para>
/// <para>Otherwise, a base64 encoded document of the indicated media type is contained in <c>Value</c>.</para>
/// </summary>
public class Content
{
    public string? Value { get; set; }

    public string? Type { get; set; }

    public string? Src { get; set; }
}
