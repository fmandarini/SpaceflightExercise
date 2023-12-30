/* TODO - Code changing
 1. Read 100 most recent articles (read 10 at time)
 2. Write articles in a file (id | title | publicationDate (GG/MM/YYYY))
 */

using System.Net.Http.Json;
using Spaceflight.Models;

var http = new HttpClient();
await using var streamWriter =
    new StreamWriter("/Users/Francesco/Documents/Ellycode/Spaceflight/Spaceflight/Articles.txt");

for (var i = 0; i < 10; i++)
{
    Console.Write($"Request N.{i + 1}\t");
    var httpResponse = await http
        .GetAsync($"https://api.spaceflightnewsapi.net/v3/articles?_limit=10&_sort=publishedAt:desc&_start={i * 10}");
    if (httpResponse.IsSuccessStatusCode)
    {
        Console.WriteLine("Server has responded");
        var articles = await httpResponse.Content.ReadFromJsonAsync<List<Article>>();
        if (articles is not null)
        {
            foreach (var article in articles)
            {
                await streamWriter.WriteLineAsync(
                    $"{article.Id} | {article.Title} | {DateTime.Parse(article.PublishedAt!):d}");
            }
        }
    }
    else
    {
        Console.WriteLine("Server has not responded");
    }
}