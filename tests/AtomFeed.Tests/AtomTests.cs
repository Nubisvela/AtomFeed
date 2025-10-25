using System.Text;
using System.Xml;
using AtomFeed.Element;

namespace AtomFeed.Tests;

public class AtomTests {
    private readonly string _feedXml =
        """
        <?xml version="1.0" encoding="utf-8"?>
        <feed xmlns="http://www.w3.org/2005/Atom">
            <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
            <title>Sample Feed</title>
            <updated>2024-11-09T08:36:48Z</updated>
            <link href="https://example.com/" />
            <category term="Feed Category" />
            <generator version="dev">AtomFeed</generator>
            <icon>/icon.jpg</icon>
            <logo>/logo.jpg</logo>
            <rights>Feed Copyright</rights>
            <subtitle>Subtitle</subtitle>
            <author>
                <name>AtomFeed</name>
                <email>me@example.com</email>
                <uri>https://example.com/</uri>
            </author>
            <contributor>
                <name>AtomFeed</name>
                <email>me@example.com</email>
                <uri>https://example.com/</uri>
            </contributor>
            <entry>
                <id>urn:uuid:01931f1f-656d-724e-942d-184a143d56cb</id>
                <title>Sample Entry</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <link href="https://example.com/" />
                <summary>Summary</summary>
                <content>Content</content>
                <category term="Entry Category" />
                <published>2024-11-09T08:36:48Z</published>
                <rights>Entry Copyright</rights>
                <author>
                    <name>AtomFeed</name>
                    <email>me@example.com</email>
                    <uri>https://example.com/</uri>
                </author>
                <contributor>
                    <name>AtomFeed</name>
                    <email>me@example.com</email>
                    <uri>https://example.com/</uri>
                </contributor>
                <source>
                    <id>https://example.org/</id>
                    <title>Example, Inc.</title>
                    <updated>2003-12-13T18:30:02Z</updated>
                </source>
            </entry>
        </feed>
        """;

    private readonly string _feedId = "urn:uuid:01931011-954d-71ee-ade5-0146811ae69f";
    private readonly string _feedTitle = "Sample Feed";
    private readonly DateTimeOffset _feedUpdated = DateTimeOffset.Parse("2024-11-09T08:36:48Z");
    private readonly string _feedLink = "https://example.com/";
    private readonly string _authorName = "AtomFeed";
    private readonly string _authorEmail = "me@example.com";
    private readonly string _authorUrl = "https://example.com/";
    private readonly string _contributorName = "AtomFeed";
    private readonly string _contributorEmail = "me@example.com";
    private readonly string _contributorUrl = "https://example.com/";
    private readonly string _feedCategory = "Feed Category";
    private readonly string _generatorValue = "AtomFeed";
    private readonly string _generatorVer = "dev";
    private readonly string _icon = "/icon.jpg";
    private readonly string _logo = "/logo.jpg";
    private readonly string _feedRights = "Feed Copyright";
    private readonly string _subtitle = "Subtitle";
    private readonly string _entryId = "urn:uuid:01931f1f-656d-724e-942d-184a143d56cb";
    private readonly string _entryTitle = "Sample Entry";
    private readonly DateTimeOffset _entryUpdated = DateTimeOffset.Parse("2024-11-09T08:36:48Z");
    private readonly string _entrySummary = "Summary";
    private readonly string _entryLink = "https://example.com/";
    private readonly string _entryContent = "Content";
    private readonly string _entryCategory = "Entry Category";
    private readonly DateTimeOffset _entryPublished = DateTimeOffset.Parse("2024-11-09T08:36:48Z");
    private readonly string _entryRights = "Entry Copyright";
    private readonly string _sourceId = "https://example.org/";
    private readonly string _sourceTitle = "Example, Inc.";
    private readonly DateTimeOffset _sourceUpdated = DateTimeOffset.Parse("2003-12-13T18:30:02Z");

    [Fact]
    public void SerializeTest() {
        // Arrange
        var feed = new Feed {
            Id = _feedId,
            Title = _feedTitle,
            Updated = _feedUpdated,
            Links = [_feedLink],
            Categories = [
                new Category {
                    Term = _feedCategory
                }
            ],
            Generator = new Generator {
                Value = _generatorValue,
                Version = _generatorVer
            },
            Authors = [
                new Author {
                    Name = _authorName,
                    Email = _authorEmail,
                    Uri = _authorUrl
                }
            ],
            Contributors = [
                new Contributor {
                    Name = _contributorName,
                    Email = _contributorEmail,
                    Uri = _contributorUrl
                }
            ],
            Icon = _icon,
            Logo = _logo,
            Rights = _feedRights,
            Subtitle = _subtitle,
            Entries = [
                new Entry {
                    Id = _entryId,
                    Title = _entryTitle,
                    Updated = _entryUpdated,
                    Links = [_entryLink],
                    Summary = _entrySummary,
                    Content = new Content {
                        Value = _entryContent
                    },
                    Categories = [
                        new Category {
                            Term = _entryCategory
                        }
                    ],
                    Rights = new Text {
                        Value = _entryRights
                    },
                    Published = _entryPublished,
                    Source = new Source {
                        Id = _sourceId,
                        Title = _sourceTitle,
                        Updated = _sourceUpdated
                    }
                }
            ]
        };

        // Act
        var xmlDoc = Atom.Serialize(feed);

        // Assert
        Assert.Equal(_feedId, xmlDoc.GetElementsByTagName("id")[0]?.InnerText);
        Assert.Equal(_feedTitle, xmlDoc.GetElementsByTagName("title")[0]?.InnerText);
        Assert.Equal(_feedUpdated.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            xmlDoc.GetElementsByTagName("updated")[0]?.InnerText);
        Assert.Equal(_feedLink, xmlDoc.GetElementsByTagName("link")[0]?.Attributes?["href"]?.Value);
        Assert.Equal(_feedCategory, xmlDoc.GetElementsByTagName("category")[0]?.Attributes?["term"]?.Value);
        Assert.Equal(_generatorValue, xmlDoc.GetElementsByTagName("generator")[0]?.InnerText);
        Assert.Equal(_generatorVer, xmlDoc.GetElementsByTagName("generator")[0]?.Attributes?["version"]?.Value);
        Assert.Equal(_icon, xmlDoc.GetElementsByTagName("icon")[0]?.InnerText);
        Assert.Equal(_logo, xmlDoc.GetElementsByTagName("logo")[0]?.InnerText);
        Assert.Equal(_feedRights, xmlDoc.GetElementsByTagName("rights")[0]?.InnerText);
        Assert.Equal(_subtitle, xmlDoc.GetElementsByTagName("subtitle")[0]?.InnerText);

        var authors = xmlDoc.GetElementsByTagName("author");
        Assert.Single(authors);
        Assert.True(authors.Item(0) is { HasChildNodes: true });
        foreach (XmlNode? node in authors.Item(0)!.ChildNodes)
            switch (node?.Name) {
                case "name":
                    Assert.Equal(_authorName, node.InnerText);
                    break;
                case "email":
                    Assert.Equal(_authorEmail, node.InnerText);
                    break;
                case "link":
                    Assert.Equal(_authorUrl, node.InnerText);
                    break;
            }

        var contributors = xmlDoc.GetElementsByTagName("contributor");
        Assert.Single(contributors);
        Assert.True(contributors.Item(0) is { HasChildNodes: true });
        foreach (XmlNode? node in contributors.Item(0)!.ChildNodes)
            switch (node?.Name) {
                case "name":
                    Assert.Equal(_contributorName, node.InnerText);
                    break;
                case "email":
                    Assert.Equal(_contributorEmail, node.InnerText);
                    break;
                case "link":
                    Assert.Equal(_contributorUrl, node.InnerText);
                    break;
            }

        var entries = xmlDoc.GetElementsByTagName("entry");
        Assert.Single(entries);
        Assert.True(entries.Item(0) is { HasChildNodes: true });
        foreach (XmlNode? node in entries.Item(0)!.ChildNodes)
            switch (node?.Name) {
                case "id":
                    Assert.Equal(_entryId, node.InnerText);
                    break;
                case "title":
                    Assert.Equal(_entryTitle, node.InnerText);
                    break;
                case "updated":
                    Assert.Equal(_entryUpdated.ToString("yyyy-MM-ddTHH:mm:ssZ"), node.InnerText);
                    break;
                case "link":
                    Assert.Equal(_entryLink, node.Attributes?["href"]?.Value);
                    break;
                case "summary":
                    Assert.Equal(_entrySummary, node.InnerText);
                    break;
                case "content":
                    Assert.Equal(_entryContent, node.InnerText);
                    break;
                case "category":
                    Assert.Equal(_entryCategory, node.Attributes?["term"]?.Value);
                    break;
                case "rights":
                    Assert.Equal(_entryRights, node.InnerText);
                    break;
                case "published":
                    Assert.Equal(_entryPublished.ToString("yyyy-MM-ddTHH:mm:ssZ"), node.InnerText);
                    break;
                case "source":
                    Assert.Equal(_sourceId, node.SelectSingleNode(".//*[name()='id']")?.InnerText);
                    Assert.Equal(_sourceTitle, node.SelectSingleNode(".//*[name()='title']")?.InnerText);
                    Assert.Equal(_sourceUpdated.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                        node.SelectSingleNode(".//*[name()='updated']")?.InnerText);
                    break;
            }
    }

    [Fact]
    public void DeserializeTest() {
        // Arrange
        var feedBytes = Encoding.UTF8.GetBytes(_feedXml);
        var feedStream = new MemoryStream(feedBytes);

        // Act
        var feedFromXml = Atom.Deserialize(_feedXml, true);
        var feedFromBytes = Atom.Deserialize(feedBytes, true);
        var feedFromStream = Atom.Deserialize(feedStream, true);

        // Assert
        AssertDeserialized(feedFromXml);
        AssertDeserialized(feedFromBytes);
        AssertDeserialized(feedFromStream);
    }

    private void AssertDeserialized(Feed? feed) {
        Assert.NotNull(feed);

        Assert.Equal(0, string.Compare(_feedId, feed.Id, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_feedTitle, feed.Title.Value, StringComparison.Ordinal));
        Assert.Equal(_feedUpdated, feed.Updated);
        Assert.Equal(0, string.Compare(_feedLink, feed.Links[0].Href, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_feedCategory, feed.Categories[0].Term, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_generatorValue, feed.Generator?.Value, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_generatorVer, feed.Generator?.Version, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_icon, feed.Icon, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_logo, feed.Logo, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_feedRights, feed.Rights?.Value, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_subtitle, feed.Subtitle?.Value, StringComparison.Ordinal));

        Assert.Single(feed.Authors);
        Assert.Equal(0, string.Compare(_authorName, feed.Authors[0].Name, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_authorEmail, feed.Authors[0].Email, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_authorUrl, feed.Authors[0].Uri, StringComparison.Ordinal));

        Assert.Single(feed.Contributors);
        Assert.Equal(0, string.Compare(_contributorName, feed.Contributors[0].Name, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_contributorEmail, feed.Contributors[0].Email, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_contributorUrl, feed.Contributors[0].Uri, StringComparison.Ordinal));

        Assert.Single(feed.Entries);
        Assert.Equal(0, string.Compare(_entryId, feed.Entries[0].Id, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_entryTitle, feed.Entries[0].Title.Value, StringComparison.Ordinal));
        Assert.Equal(_entryUpdated, feed.Entries[0].Updated);
        Assert.Equal(0, string.Compare(_entryLink, feed.Entries[0].Links[0].Href, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_entrySummary, feed.Entries[0].Summary?.Value, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_entryContent, feed.Entries[0].Content?.Value, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_entryCategory, feed.Entries[0].Categories[0].Term, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_entryRights, feed.Entries[0].Rights?.Value, StringComparison.Ordinal));
        Assert.Equal(_entryPublished, feed.Entries[0].Published);
        Assert.Equal(0, string.Compare(_sourceId, feed.Entries[0].Source?.Id, StringComparison.Ordinal));
        Assert.Equal(0, string.Compare(_sourceTitle, feed.Entries[0].Source?.Title.Value, StringComparison.Ordinal));
        Assert.Equal(_sourceUpdated, feed.Entries[0].Source?.Updated);
    }
}