using System.Text;
using System.Xml;
using AtomFeed.Element;
using AtomFeed.Serialization;

namespace AtomFeed;

public static class Atom {
    /// <summary>
    /// Serialize a feed to an XML document.
    /// </summary>
    /// <param name="feed">Feed object.</param>
    /// <param name="encoding">XML declaration encoding.</param>
    /// <returns></returns>
    public static XmlDocument Serialize(Feed feed, Encoding? encoding = null) {
        return Serializer.SerializeFeed(feed, encoding);
    }

    /// <summary>
    /// Deserialize an XML document string to feed instance.
    /// </summary>
    /// <param name="xml">XML string.</param>
    /// <param name="strict">Strict mode. If the <c>strict</c> is <c>true</c>, the XML document syntax must be
    /// fully compliant with the W3C validation, otherwise an exception will be thrown. If the
    /// <c>strict</c> is <c>false</c>, those elements which are not compliant will be set to
    /// <c>null</c> or <c>default</c>.</param>
    /// <returns>Feed instance. If the <c>strict</c> is <c>false</c> and the <c>xml</c> is invalid,
    /// then <c>null</c> is returned.</returns>
    public static Feed? Deserialize(string xml, bool strict = false) {
        return Serializer.DeserializeFeed(xml, strict);
    }

    /// <summary>
    /// Deserialize an XML document buffer to feed instance.
    /// </summary>
    /// <param name="buffer">XML buffer.</param>
    /// <param name="strict">Strict mode.</param>
    /// <returns>Feed instance. If the <c>strict</c> is <c>false</c> and the <c>buffer</c> is invalid,
    /// then <c>null</c> is returned.</returns>
    /// <seealso cref="Deserialize(string,bool)"/>
    public static Feed? Deserialize(ReadOnlySpan<byte> buffer, bool strict = false) {
        return Serializer.DeserializeFeed(buffer, strict);
    }

    /// <summary>
    /// Deserialize an XML document stream to feed instance.
    /// </summary>
    /// <param name="stream">XML stream.</param>
    /// <param name="strict">Strict mode.</param>
    /// <returns>Feed instance. If the <c>strict</c> is <c>false</c> and the <c>stream</c> is invalid,
    /// then <c>null</c> is returned.</returns>
    /// <seealso cref="Deserialize(string,bool)"/>
    public static Feed? Deserialize(Stream stream, bool strict = false) {
        return Serializer.DeserializeFeed(stream, strict);
    }
}