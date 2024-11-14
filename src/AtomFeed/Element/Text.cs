namespace AtomFeed.Element;

/// <summary>
/// <c>title</c>, <c>subtitle</c>, <c>summary</c>, <c>content</c>, and
/// <c>rights</c> contain human-readable text, usually in small quantities.
/// The type attribute determines how this information is encoded (default="text").
/// </summary>
public class Text
{
    public required string Value { get; set; }

    public TextType Type { get; set; } = TextType.Text;

    public static implicit operator Text(string value)
    {
        return new Text
        {
            Value = value,
            Type = TextType.Text
        };
    }

    public override string ToString() => Value;
}

public enum TextType
{
    /// <summary>
    /// Plain text with no entity escaped html.
    /// </summary>
    Text,

    /// <summary>
    /// Entity escaped html.
    /// </summary>
    Html,

    /// <summary>
    /// Inline xhtml, wrapped in a <c>div</c> element.
    /// </summary>
    Xhtml
}
