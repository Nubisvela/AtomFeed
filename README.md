# AtomFeed

[![latest version](https://img.shields.io/nuget/v/AtomFeed.svg)](https://www.nuget.org/packages/AtomFeed)
[![downloads](https://img.shields.io/nuget/dt/AtomFeed.svg)](https://www.nuget.org/packages/AtomFeed)
[![tests status](https://github.com/chrishyze/AtomFeed/actions/workflows/tests.yml/badge.svg?branch=main)](https://github.com/chrishyze/AtomFeed/actions/workflows/tests.yml)
[![license](https://img.shields.io/badge/license-MIT-informational)](https://www.nuget.org/packages/AtomFeed)

The Atom Syndication Format is an XML language used for web feeds.

This project is a .NET implement of Atom 1.0. 

## Features

- Serialize atom feed into XML document
- Deserialize atom feed from XML
- Follows and conforms to [W3C Atom feed validation](https://validator.w3.org/feed/docs/atom.html)
- Supports .NET 8 and .NET 9, Native AOT compatible
- Zero dependencies

## Get Started

You may want to learn about the Atom from the links below:

- [Atom (web standard) - Wikipedia](https://en.wikipedia.org/wiki/Atom_(web_standard)) (Include a comparison to the RSS 2.0) 
- [The Atom Syndication Format (RFC 4287) - IETF](https://datatracker.ietf.org/doc/html/rfc4287)

Most of the modern feed readers support Atom.

You can use this library to build a feed server, or build an app to handle Atom feeds.

## Installation

```shell
dotnet add package AtomFeed
```

## Usage

### Basic usage

Serialize:

```csharp
using AtomFeed;
using AtomFeed.Element;

var feed = new Feed
{
    Id = "urn:uuid:" + Guid.NewGuid(),
    Title = "My Blog Feed",
    Updated = DateTimeOffset.UtcNow,
    Entries =
    [
        new Entry
        {
            Id = "urn:uuid:" + Guid.NewGuid(),
            Title = "My First Article",
            Updated = DateTimeOffset.UtcNow
        }
    ]
};

var xmlDocument = Atom.Serialize(feed);
```

Deserialize:

```csharp
using AtomFeed;

const string xml =
    """
    <?xml version="1.0" encoding="utf-8"?>
    <feed xmlns="http://www.w3.org/2005/Atom">
        <id>urn:uuid:01931011-954d-71ee-ade5-0146811ae69f</id>
        <title>Sample Feed</title>
        <updated>2024-11-09T08:36:48Z</updated>
    </feed>
    """;

var feed = Atom.Deserialize(xml);
```

### Strict mode in deserialization

Normally, the strict mode is disabled, when the giving feed is invalid, the parser will return `null`.

If the strict mode is enabled, the parser will throw an exception.

The strict mode can be used to validate the feed, it follows the [W3C Atom feed validation](https://validator.w3.org/feed/docs/atom.html).

```csharp
var xml = string.Empty;

// A System.Data.ArgumentException will be thrown,
// because the feed is empty.
var feed = Atom.Deserialize(xml, true);
```

```csharp
const string xml =
    """
    <?xml version="1.0" encoding="utf-8"?>
    <feed xmlns="http://www.w3.org/2005/Atom">
    </feed>
    """;

// A System.Data.ConstraintException will be thrown,
// because the feed <id> tag is missing.
var feed = Atom.Deserialize(xml, true);
```

See more in the [DeserializeStrictModeTests.cs](https://github.com/chrishyze/AtomFeed/blob/main/tests/AtomFeed.Tests/DeserializeStrictModeTests.cs)

## Examples

- [Feed Server Example](https://github.com/chrishyze/AtomFeed/tree/main/examples/FeedServer)
  This example demonstrates a feed server using the AtomFeed library.
- [Feed Reader Example](https://github.com/chrishyze/AtomFeed/tree/main/examples/FeedReader)
  This example demonstrates how to use the AtomFeed library to parse feed in a Console app.

## License

The MIT License
