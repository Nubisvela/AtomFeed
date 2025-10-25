using AtomFeed.Serialization;

namespace AtomFeed.Tests;

public class DeserializeTests {
    [Fact]
    public void MinimumFeedTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
            </feed>
            """;

        // Act
        var feed = Serializer.DeserializeFeed(xml);

        // Assert
        Assert.NotNull(feed);
        Assert.Null(feed.Icon);
        Assert.Null(feed.Logo);
        Assert.Null(feed.Generator);
        Assert.Null(feed.Rights);
        Assert.Null(feed.Subtitle);
        Assert.Empty(feed.Authors);
        Assert.Empty(feed.Contributors);
        Assert.Empty(feed.Links);
        Assert.Empty(feed.Categories);
        Assert.Empty(feed.Entries);
    }

    [Fact]
    public void InvalidFeedTest() {
        // Arrange
        const string xml1 = "";
        const string xml2 =
            """
            <?xml version="1.0" encoding="utf-8"?>
            """;

        // Act
        var feed1 = Serializer.DeserializeFeed(xml1);
        var feed2 = Serializer.DeserializeFeed(xml2);

        // Assert
        Assert.Null(feed1);
        Assert.Null(feed2);
    }
}