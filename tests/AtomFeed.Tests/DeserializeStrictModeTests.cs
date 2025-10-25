using System.Data;

namespace AtomFeed.Tests;

public class DeserializeStrictModeTests {
    [Fact]
    public void EmptySourceTest() {
        // Arrange
        var xmlString = string.Empty;
        var xmlBytes = Array.Empty<byte>();
        var xmlStream = new MemoryStream();

        // Act
        var stringException = Assert.Throws<ArgumentException>(() => Atom.Deserialize(xmlString, true));
        var bytesException = Assert.Throws<ArgumentException>(() => Atom.Deserialize(xmlBytes, true));
        var streamException = Assert.Throws<ArgumentException>(() => Atom.Deserialize(xmlStream, true));

        // Assert
        Assert.Equal("AtomFeed: xml string can not be empty", stringException.Message);
        Assert.Equal("AtomFeed: xml buffer can not be empty", bytesException.Message);
        Assert.Equal("AtomFeed: xml stream can not be empty", streamException.Message);
    }

    [Fact]
    public void NoFeeIdTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: feed id is missing", caughtException.Message);
    }

    [Fact]
    public void NoFeedTitleTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: feed title is missing", caughtException.Message);
    }

    [Fact]
    public void NoFeedUpdatedTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: feed updated is missing", caughtException.Message);
    }

    [Fact]
    public void InvalidFeedUpdatedTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>invalid</updated>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: invalid feed updated", caughtException.Message);
    }

    [Fact]
    public void NoEntryIdTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <title>Sample Entry</title>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: entry id is missing", caughtException.Message);
    }

    [Fact]
    public void NoEntryTitleTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: entry title is missing", caughtException.Message);
    }

    [Fact]
    public void NoEntryUpdatedTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                    <title>Sample Entry</title>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: entry updated is missing", caughtException.Message);
    }

    [Fact]
    public void InvalidEntryUpdatedTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                    <title>Sample Entry</title>
                    <updated>invalid</updated>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: invalid entry updated", caughtException.Message);
    }

    [Fact]
    public void NoFeedAuthorNameTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <author>
                    <email>me@example.com</email>
                </author>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: feed author name is missing", caughtException.Message);
    }

    [Fact]
    public void NoEntryAuthorNameTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                    <title>Sample Entry</title>
                    <updated>2024-11-09T08:36:48Z</updated>
                    <author>
                        <email>me@example.com</email>
                    </author>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: entry author name is missing", caughtException.Message);
    }

    [Fact]
    public void NoFeedLinkHrefTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <link rel=""/>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: feed link href attribute is missing", caughtException.Message);
    }

    [Fact]
    public void NoEntryLinkHrefTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                    <title>Sample Entry</title>
                    <updated>2024-11-09T08:36:48Z</updated>
                    <link rel=""/>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: entry link href attribute is missing", caughtException.Message);
    }

    [Fact]
    public void NoFeedCategoryTermTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <category label=""/>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: feed category term attribute is missing", caughtException.Message);
    }

    [Fact]
    public void NoEntryCategoryTermTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                    <title>Sample Entry</title>
                    <updated>2024-11-09T08:36:48Z</updated>
                    <category label=""/>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: entry category term attribute is missing", caughtException.Message);
    }

    [Fact]
    public void NoGeneratorNameTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <generator>
                </generator>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: generator name is missing", caughtException.Message);
    }

    [Fact]
    public void NoEntrySourceIdTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                    <title>Sample Entry</title>
                    <updated>2024-11-09T08:36:48Z</updated>
                    <source>
                    </source>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: entry source id is missing", caughtException.Message);
    }

    [Fact]
    public void NoEntrySourceTitleTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                    <title>Sample Entry</title>
                    <updated>2024-11-09T08:36:48Z</updated>
                    <source>
                        <id>https://example.com/</id>
                    </source>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: entry source title is missing", caughtException.Message);
    }

    [Fact]
    public void NoEntrySourceUpdatedTest() {
        // Arrange
        const string xml =
            """
            <?xml version="1.0" encoding="utf-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
                <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                <title>Sample Feed</title>
                <updated>2024-11-09T08:36:48Z</updated>
                <entry>
                    <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
                    <title>Sample Entry</title>
                    <updated>2024-11-09T08:36:48Z</updated>
                    <source>
                        <id>https://example.com/</id>
                        <title>Sample Source</title>
                    </source>
                </entry>
            </feed>
            """;

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Deserialize(xml, true));

        // Assert
        Assert.Equal("AtomFeed: entry source updated is missing", caughtException.Message);
    }
}