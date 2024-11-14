using System.Text;
using System.Xml;
using AtomFeed;
using AtomFeed.Element;

var builder = WebApplication.CreateSlimBuilder(args);
var app = builder.Build();
app.MapGet("/", Feed);
app.Run();
return;

IResult Feed()
{
    // Create an Atom feed.
    var feed = new Feed
    {
        Id = "urn:uuid:" + Guid.NewGuid(),
        Title = "Feed Example",
        Updated = DateTimeOffset.UtcNow,
        Links =
        [
            new Link
            {
                Href = "https://example.com/",
                Type = "text/html"
            }
        ],
        Authors =
        [
            new Author
            {
                Name = "Example Author",
                Email = "me@example.com",
                Uri = "https://example.com/"
            }
        ],
        Generator = new Generator
        {
            Value = "AtomFeed",
            Uri = "https://github.com/chrishyze/AtomFeed",
            Version = "latest"
        },
        Icon = "/icon.png",
        Logo = "/logo.png",
        Entries =
        [
            new Entry
            {
                Id = "urn:uuid:" + Guid.NewGuid(),
                Title = "The First Article",
                Updated = DateTimeOffset.UtcNow,
                Links =
                [
                    new Link
                    {
                        Href = "https://example.com/first-article",
                        Type = "text/html"
                    }
                ],
                Summary = "The summary of the first article.",
                Content = new Content
                {
                    Type = "text/plain",
                    Value = "The content of the first article."
                }
            }
        ]
    };

    // Serialize the feed to XML.
    var xmlDocument = Atom.Serialize(feed);
    using var stream = new MemoryStream();
    using var xmlWriter = new XmlTextWriter(stream, Encoding.UTF8);
    xmlDocument.Save(xmlWriter);
    var xml = Encoding.UTF8.GetString(stream.ToArray());

    // Respond with XML.
    return TypedResults.Text(xml, Constants.AtomMimetype, Encoding.UTF8);
}
