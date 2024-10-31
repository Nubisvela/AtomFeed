# AtomFeed

The Atom Syndication Format is an XML language used for web feeds.

This project is a .NET implement of [The Atom Syndication Format (RFC 4287)](https://datatracker.ietf.org/doc/html/rfc4287). 
Follows and conforms to [W3C Atom feed validation](https://validator.w3.org/feed/docs/atom.html).

## Usage

Serialize:

```csharp
using AtomFeed;
using AtomFeed.Element;

var feed = new Feed
{
    Id = "urn:uuid:" + Guid.NewGuid().ToString(),
    Title = "My Blog Feed",
    Updated = DateTimeOffset.UtcNow,
    Entries =
    [
        new Entry
        {
            Id = "urn:uuid:" + Guid.NewGuid().ToString(),
            Title = "My First Article",
            Updated = DateTimeOffset.UtcNow
        }
    ]
};

var xmlDocument = AtomFeed.Serialize(feed);
```

Deserialize:

```csharp
using AtomFeed;

var feed = AtomFeed.Deserialize(xml);
```

## License

The MIT License
