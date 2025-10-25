using AtomFeed;

var httpClient = new HttpClient { BaseAddress = new Uri("https://www.nuget.org") };

Console.WriteLine("GET https://www.nuget.org/packages/AtomFeed/atom.xml");
Console.WriteLine("Sending HTTP request...");
using var response = await httpClient.GetAsync("/packages/AtomFeed/atom.xml");
response.EnsureSuccessStatusCode();

var atomResponse = await response.Content.ReadAsStringAsync();
Console.WriteLine("HTTP response received.");

var feed = Atom.Deserialize(atomResponse);

if (feed == null) {
    Console.WriteLine("Failed to deserialize feed.");
    return;
}

Console.WriteLine("Deserialized feed:");

Console.WriteLine($"> Feed Id: {feed.Id}");
Console.WriteLine($"> Feed Title: {feed.Title.Value}");
Console.WriteLine($"> Feed Subtitle: {feed.Subtitle}");
Console.WriteLine($"> Feed Updated: {feed.Updated:yyyy-MM-ddTHH:mm:ssZ}");

Console.WriteLine("> Links:");
for (var i = 0; i < feed.Links.Count; i++) {
    var link = feed.Links[i];
    var prefix = i == feed.Links.Count - 1 ? "  \u2514\u2500" : "  \u251c\u2500";
    Console.WriteLine($"{prefix} Links[{i}] Href: {link.Href}, Rel: {link.Relation}, Type: {link.Type}");
}

Console.WriteLine("> Entries:");
for (var i = 0; i < feed.Entries.Count; i++) {
    var entry = feed.Entries[i];
    var prefix = i == feed.Entries.Count - 1 ? "  \u2514\u2500" : "  \u251c\u2500";
    Console.WriteLine($"{prefix} Entries[{i}]");
    var subPrefix = i == feed.Entries.Count - 1 ? "     " : "  \u2502  ";
    Console.WriteLine($"{subPrefix}\u251c\u2500 Id: {entry.Id}");
    Console.WriteLine($"{subPrefix}\u251c\u2500 Title: {entry.Title}");
    Console.WriteLine($"{subPrefix}\u251c\u2500 Published: {entry.Published:yyyy-MM-ddTHH:mm:ssZ}");
    Console.WriteLine($"{subPrefix}\u251c\u2500 Updated: {entry.Updated:yyyy-MM-ddTHH:mm:ssZ}");
    Console.WriteLine($"{subPrefix}\u251c\u2500 Author: {entry.Authors[0].Name} <{entry.Authors[0].Uri}>");
    Console.WriteLine($"{subPrefix}\u251c\u2500 Link: {entry.Links[0].Href}");
    Console.WriteLine($"{subPrefix}\u2514\u2500 Content: {entry.Content?.Value}");
}