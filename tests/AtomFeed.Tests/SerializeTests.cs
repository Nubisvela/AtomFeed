using System.Data;
using AtomFeed.Element;

namespace AtomFeed.Tests;

public class SerializeTests {
    [Fact]
    public void EmptyFeedIdTest() {
        // Arrange
        var feed = new Feed {
            Id = "",
            Title = "",
            Updated = DateTimeOffset.UtcNow
        };

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Serialize(feed));

        // Assert
        Assert.Equal("AtomFeed: feed id can not be empty", caughtException.Message);
    }

    [Fact]
    public void EmptyFeedTitleTest() {
        // Arrange
        var feed = new Feed {
            Id = "id",
            Title = "",
            Updated = DateTimeOffset.UtcNow
        };

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Serialize(feed));

        // Assert
        Assert.Equal("AtomFeed: feed title can not be empty", caughtException.Message);
    }

    [Fact]
    public void EmptyEntryIdTest() {
        // Arrange
        var feed = new Feed {
            Id = "id",
            Title = "title",
            Updated = DateTimeOffset.UtcNow,
            Entries = [
                new Entry {
                    Id = "",
                    Title = "",
                    Updated = DateTimeOffset.UtcNow
                }
            ]
        };

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Serialize(feed));

        // Assert
        Assert.Equal("AtomFeed: entry id can not be empty", caughtException.Message);
    }

    [Fact]
    public void EmptyEntryTitleTest() {
        // Arrange
        var feed = new Feed {
            Id = "id",
            Title = "title",
            Updated = DateTimeOffset.UtcNow,
            Entries = [
                new Entry {
                    Id = "id",
                    Title = "",
                    Updated = DateTimeOffset.UtcNow
                }
            ]
        };

        // Act
        var caughtException = Assert.Throws<ConstraintException>(() => Atom.Serialize(feed));

        // Assert
        Assert.Equal("AtomFeed: entry title can not be empty", caughtException.Message);
    }
}