# AtomFeed

[![](https://img.shields.io/nuget/v/AtomFeed.svg)](https://www.nuget.org/packages/AtomFeed)
[![](https://github.com/chrishyze/AtomFeed/actions/workflows/tests.yml/badge.svg?branch=main)](https://github.com/chrishyze/AtomFeed/workflows/Tests)
[![](https://img.shields.io/badge/license-MIT-informational)](https://www.nuget.org/packages/AtomFeed)

The Atom Syndication Format is an XML language used for web feeds.

This project is a .NET implement of [The Atom Syndication Format (RFC 4287)](https://datatracker.ietf.org/doc/html/rfc4287). 

## Features

- Serialize atom feed into XML document
- Deserialize atom feed from XML
- Follows and conforms to [W3C Atom feed validation](https://validator.w3.org/feed/docs/atom.html).

## Installation

```shell
dotnet add package AtomFeed
```

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
