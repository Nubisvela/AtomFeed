using System.Data;
using System.Text;
using System.Xml;
using AtomFeed.Element;

namespace AtomFeed.Serialization;

public static partial class Serializer {
    /// <summary>
    /// Available XML node names for <see cref="Text"/> object.
    /// </summary>
    private static readonly string[] TextElementNames = ["title", "summary", "content", "rights"];

    /// <summary>
    /// Serialize a feed object to an XML document.
    /// </summary>
    /// <param name="feed">Feed object.</param>
    /// <param name="encoding">The value of the encoding attribute.
    /// <seealso cref="XmlDocument.CreateXmlDeclaration"/></param>
    /// <returns>The XML document object.</returns>
    /// <exception cref="ConstraintException"></exception>
    public static XmlDocument SerializeFeed(Feed feed, Encoding? encoding = null) {
        // Feed id is required.
        if (string.IsNullOrEmpty(feed.Id))
            throw new ConstraintException("AtomFeed: feed id can not be empty");

        // Feed title is required.
        if (string.IsNullOrEmpty(feed.Title.Value))
            throw new ConstraintException("AtomFeed: feed title can not be empty");

        var document = new XmlDocument();
        document.CreateXmlDeclaration("1.0", encoding?.BodyName, null);

        var manager = new XmlNamespaceManager(document.NameTable);
        manager.AddNamespace("atom", Constants.AtomNamespace);

        var feedElement = document.CreateElement("feed");
        feedElement.SetAttribute("xmlns", Constants.AtomNamespace);

        // Feed id node.
        var idElement = document.CreateElement("id");
        idElement.InnerText = feed.Id;
        feedElement.AppendChild(idElement);

        // Feed title node.
        var titleElement = SerializeText(feed.Title, "title", document);
        feedElement.AppendChild(titleElement);

        // Feed updated node.
        var updatedElement = document.CreateElement("updated");
        updatedElement.InnerText = feed.Updated.ToString("yyyy-MM-ddTHH:mm:ssZ");
        feedElement.AppendChild(updatedElement);

        // Serialize feed authors (if any).
        foreach (var authorElement in SerializeAuthors(feed.Authors, document))
            feedElement.AppendChild(authorElement);

        // Serialize feed links (if any).
        foreach (var linkElement in SerializeLinks(feed.Links, document))
            feedElement.AppendChild(linkElement);

        // Serialize feed contributors (if any).
        foreach (var contributorElement in SerializeContributors(feed.Contributors, document))
            feedElement.AppendChild(contributorElement);

        // Serialize feed categories (if any).
        foreach (var categoryElement in SerializeCategories(feed.Categories, document))
            feedElement.AppendChild(categoryElement);

        // Serialize feed generator (if any).
        if (feed.Generator is not null)
            feedElement.AppendChild(SerializeGenerator(feed.Generator, document));

        // Feed icon node.
        if (feed.Icon is not null) {
            var iconElement = document.CreateElement("icon");
            iconElement.InnerText = feed.Icon;
            feedElement.AppendChild(iconElement);
        }

        // Feed logo node.
        if (feed.Logo is not null) {
            var logoElement = document.CreateElement("logo");
            logoElement.InnerText = feed.Logo;
            feedElement.AppendChild(logoElement);
        }

        // Serialize feed rights (if any).
        if (feed.Rights is not null)
            feedElement.AppendChild(SerializeText(feed.Rights, "rights", document));

        // Feed subtitle node.
        if (feed.Subtitle is not null) {
            var subtitleElement = document.CreateElement("subtitle");
            subtitleElement.InnerText = feed.Subtitle.Value;
            feedElement.AppendChild(subtitleElement);
        }

        // Serialize feed entries.
        foreach (var entryElement in feed.Entries.Select(entry => SerializeEntry(entry, document)))
            feedElement.AppendChild(entryElement);

        document.AppendChild(feedElement);

        return document;
    }

    /// <summary>
    /// Serialize entry.
    /// </summary>
    /// <param name="entry">Entry object.</param>
    /// <param name="document">Root XML document.</param>
    /// <returns>The XML element.</returns>
    /// <exception cref="ConstraintException"></exception>
    private static XmlElement SerializeEntry(Entry entry, XmlDocument document) {
        // Entry id is required.
        if (string.IsNullOrEmpty(entry.Id))
            throw new ConstraintException("AtomFeed: entry id can not be empty");

        // Entry title is required.
        if (string.IsNullOrEmpty(entry.Title.Value))
            throw new ConstraintException("AtomFeed: entry title can not be empty");

        var entryElement = document.CreateElement("entry");

        // Entry id node.
        var idElement = document.CreateElement("id");
        idElement.InnerText = entry.Id;
        entryElement.AppendChild(idElement);

        // Entry title node.
        var titleElement = document.CreateElement("title");
        titleElement.InnerText = entry.Title.Value;
        entryElement.AppendChild(titleElement);

        // Entry updated node.
        var updatedElement = document.CreateElement("updated");
        updatedElement.InnerText = entry.Updated.ToString("yyyy-MM-ddTHH:mm:ssZ");
        entryElement.AppendChild(updatedElement);

        // Serialize feed authors (if any).
        foreach (var authorElement in SerializeAuthors(entry.Authors, document))
            entryElement.AppendChild(authorElement);

        // Serialize entry content (if any).
        if (entry.Content is not null)
            entryElement.AppendChild(SerializeContent(entry.Content, document));

        // Serialize entry links (if any).
        foreach (var linkElement in SerializeLinks(entry.Links, document))
            entryElement.AppendChild(linkElement);

        // Serialize entry summary (if any).
        if (entry.Summary is not null)
            entryElement.AppendChild(SerializeText(entry.Summary, "summary", document));

        // Serialize entry contributors (if any).
        foreach (var contributorElement in SerializeContributors(entry.Contributors, document))
            entryElement.AppendChild(contributorElement);

        // Serialize entry categories (if any).
        foreach (var categoryElement in SerializeCategories(entry.Categories, document))
            entryElement.AppendChild(categoryElement);

        // Entry published node.
        if (entry.Published is not null) {
            var publishedElement = document.CreateElement("published");
            publishedElement.InnerText = entry.Published?.ToString("yyyy-MM-ddTHH:mm:ssZ")!;
            entryElement.AppendChild(publishedElement);
        }

        // Serialize entry rights (if any).
        if (entry.Rights is not null)
            entryElement.AppendChild(SerializeText(entry.Rights, "rights", document));

        // Serialize feed source (if any).
        if (entry.Source is not null)
            entryElement.AppendChild(SerializeSource(entry.Source, document));

        return entryElement;
    }

    /// <summary>
    /// Serialize authors.
    /// </summary>
    /// <param name="authors">Author list.</param>
    /// <param name="document">Root XML document.</param>
    /// <returns>XML element list.</returns>
    /// <exception cref="ConstraintException"></exception>
    private static List<XmlElement> SerializeAuthors(List<Author> authors, XmlDocument document) {
        List<XmlElement> authorElements = [];

        foreach (var author in authors) {
            // Author name is required.
            if (string.IsNullOrEmpty(author.Name))
                throw new ConstraintException("AtomFeed: author name can not be empty");

            var authorElement = document.CreateElement("author");

            // Author name node.
            var nameElement = document.CreateElement("name");
            nameElement.InnerText = author.Name;
            authorElement.AppendChild(nameElement);

            // Author email node.
            if (author.Email is not null) {
                var emailElement = document.CreateElement("email");
                emailElement.InnerText = author.Email;
                authorElement.AppendChild(emailElement);
            }

            // Author URL node.
            if (author.Uri is not null) {
                var urlElement = document.CreateElement("url");
                urlElement.InnerText = author.Uri;
                authorElement.AppendChild(urlElement);
            }

            authorElements.Add(authorElement);
        }

        return authorElements;
    }

    /// <summary>
    /// Serialize links.
    /// </summary>
    /// <param name="links">Link list.</param>
    /// <param name="document">Root XML document.</param>
    /// <returns>Link element list.</returns>
    /// <exception cref="ConstraintException"></exception>
    private static List<XmlElement> SerializeLinks(List<Link> links, XmlDocument document) {
        List<XmlElement> linkElements = [];

        foreach (var link in links) {
            // Link href is required.
            if (string.IsNullOrEmpty(link.Href))
                throw new ConstraintException("AtomFeed: link href can not be empty");

            var linkElement = document.CreateElement("link");

            // Link href attribute.
            var hrefAttribute = document.CreateAttribute("href");
            hrefAttribute.Value = link.Href;
            linkElement.Attributes.Append(hrefAttribute);

            // Link rel attribute.
            if (link.Relation is not null) {
                var relationAttribute = document.CreateAttribute("rel");
                relationAttribute.Value = link.Relation;
                linkElement.Attributes.Append(relationAttribute);
            }

            // Link type attribute.
            if (link.Type is not null) {
                var typeAttribute = document.CreateAttribute("type");
                typeAttribute.Value = link.Type;
                linkElement.Attributes.Append(typeAttribute);
            }

            // Link hreflang attribute.
            if (link.HrefLanguage is not null) {
                var langAttribute = document.CreateAttribute("hreflang");
                langAttribute.Value = link.HrefLanguage;
                linkElement.Attributes.Append(langAttribute);
            }

            // Link title attribute.
            if (link.Title is not null) {
                var titleAttribute = document.CreateAttribute("title");
                titleAttribute.Value = link.Title;
                linkElement.Attributes.Append(titleAttribute);
            }

            // Link length attribute.
            if (link.Length is not null) {
                var lengthAttribute = document.CreateAttribute("length");
                lengthAttribute.Value = link.Length.ToString();
                linkElement.Attributes.Append(lengthAttribute);
            }

            linkElements.Add(linkElement);
        }

        return linkElements;
    }

    /// <summary>
    /// Serialize categories.
    /// </summary>
    /// <param name="categories">Category list.</param>
    /// <param name="document">Root XML document.</param>
    /// <returns>Category element list.</returns>
    /// <exception cref="ConstraintException"></exception>
    private static List<XmlElement> SerializeCategories(List<Category> categories, XmlDocument document) {
        List<XmlElement> categoryElements = [];

        foreach (var category in categories) {
            // Category term is required.
            if (string.IsNullOrEmpty(category.Term))
                throw new ConstraintException("AtomFeed: category term can not be empty");

            var categoryElement = document.CreateElement("category");

            // Category term attribute.
            var termAttribute = document.CreateAttribute("term");
            termAttribute.Value = category.Term;
            categoryElement.Attributes.Append(termAttribute);

            // Category scheme attribute.
            if (category.Scheme is not null) {
                var schemeAttribute = document.CreateAttribute("scheme");
                schemeAttribute.Value = category.Scheme;
                categoryElement.Attributes.Append(schemeAttribute);
            }

            // Category label attribute.
            if (category.Label is not null) {
                var labelAttribute = document.CreateAttribute("label");
                labelAttribute.Value = category.Label;
                categoryElement.Attributes.Append(labelAttribute);
            }

            categoryElements.Add(categoryElement);
        }

        return categoryElements;
    }

    /// <summary>
    /// Serialize contributors.
    /// </summary>
    /// <param name="contributors">Contributor list.</param>
    /// <param name="document">Root XML document.</param>
    /// <returns>Contributor element list.</returns>
    /// <exception cref="ConstraintException"></exception>
    private static List<XmlElement> SerializeContributors(List<Contributor> contributors, XmlDocument document) {
        List<XmlElement> contributorElements = [];

        foreach (var contributor in contributors) {
            // Contributor name is required.
            if (string.IsNullOrEmpty(contributor.Name))
                throw new ConstraintException("AtomFeed: contributor name can not be empty");

            var contributorElement = document.CreateElement("contributor");

            // Contributor name node.
            var nameElement = document.CreateElement("name");
            nameElement.InnerText = contributor.Name;
            contributorElement.AppendChild(nameElement);

            // Contributor email node.
            if (contributor.Email is not null) {
                var emailElement = document.CreateElement("email");
                emailElement.InnerText = contributor.Email;
                contributorElement.AppendChild(emailElement);
            }

            // Contributor URL node.
            if (contributor.Uri is not null) {
                var urlElement = document.CreateElement("url");
                urlElement.InnerText = contributor.Uri;
                contributorElement.AppendChild(urlElement);
            }

            contributorElements.Add(contributorElement);
        }

        return contributorElements;
    }

    /// <summary>
    /// Serialize generator.
    /// </summary>
    /// <param name="generator">Generator object.</param>
    /// <param name="document">Root XML document.</param>
    /// <returns>Generator element.</returns>
    private static XmlElement SerializeGenerator(Generator generator, XmlDocument document) {
        var generatorElement = document.CreateElement("generator");
        generatorElement.InnerText = generator.Value;

        // Generator version attribute.
        if (generator.Version is not null) {
            var versionAttribute = document.CreateAttribute("version");
            versionAttribute.Value = generator.Version;
            generatorElement.Attributes.Append(versionAttribute);
        }

        // Generator URI attribute.
        if (generator.Uri is not null) {
            var uriAttribute = document.CreateAttribute("uri");
            uriAttribute.Value = generator.Uri;
            generatorElement.Attributes.Append(uriAttribute);
        }

        return generatorElement;
    }

    /// <summary>
    /// Serialize text to given element name.
    /// </summary>
    /// <param name="text">Text object.</param>
    /// <param name="name">Element name, <c>title</c>, <c>summary</c>, <c>content</c>, or <c>rights</c>.</param>
    /// <param name="document">Root XML document.</param>
    /// <returns>The XML element.</returns>
    /// <exception cref="NotSupportedException"></exception>
    private static XmlElement SerializeText(Text text, string name, XmlDocument document) {
        if (!TextElementNames.Contains(name))
            throw new NotSupportedException($"AtomFeed: Unsupported text element name '{name}'.");

        var textElement = document.CreateElement(name);
        textElement.InnerText = text.Value;

        // Text type attribute is redundant and will be ignored.
        if (text.Type == TextType.Text) return textElement;

        var typeAttribute = document.CreateAttribute("type");
        typeAttribute.Value = text.Type switch {
            TextType.Text => "text",
            TextType.Html => "html",
            TextType.Xhtml => "xhtml",
            _ => throw new NotSupportedException($"AtomFeed: Unsupported text type '{text.Type}'.")
        };
        textElement.Attributes.Append(typeAttribute);

        return textElement;
    }

    /// <summary>
    /// Serialize content of entry.
    /// </summary>
    /// <param name="content">Content object.</param>
    /// <param name="document">Root XML document.</param>
    /// <returns>Content element.</returns>
    private static XmlElement SerializeContent(Content content, XmlDocument document) {
        var contentElement = document.CreateElement("content");
        contentElement.InnerText = content.Value ?? "";

        // Text type attribute is redundant and will be ignored.
        if (content.Type is not null && content.Type != "text") {
            var typeAttribute = document.CreateAttribute("type");
            typeAttribute.Value = content.Type;
            contentElement.Attributes.Append(typeAttribute);
        }

        // Content src attribute.
        if (content.Src is not null) {
            var srcAttribute = document.CreateAttribute("src");
            srcAttribute.Value = content.Src;
            contentElement.Attributes.Append(srcAttribute);
        }

        return contentElement;
    }

    /// <summary>
    /// Serialize source.
    /// </summary>
    /// <param name="source">Source object.</param>
    /// <param name="document">Root XML document.</param>
    /// <returns>Source element.</returns>
    private static XmlElement SerializeSource(Source source, XmlDocument document) {
        var sourceElement = document.CreateElement("source");

        // Source id node.
        var idElement = document.CreateElement("id");
        idElement.InnerText = source.Id;
        sourceElement.AppendChild(idElement);

        // Source title node.
        var titleElement = SerializeText(source.Title, "title", document);
        sourceElement.AppendChild(titleElement);

        // Source updated node.
        var updatedElement = document.CreateElement("updated");
        updatedElement.InnerText = source.Updated.ToString("yyyy-MM-ddTHH:mm:ssZ");
        sourceElement.AppendChild(updatedElement);

        return sourceElement;
    }
}