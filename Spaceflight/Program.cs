/* TODO - Per domani
 1. Rendere la casse file writer generica
 2. Spostare i metodi per la lettura di articoli e blog in una
    nuova classe SpaceFlightAPIClient
 3. Rendere i path relativi
 4. (Bonus) Creare degli overload asyncEnumerable dei metodi di SpaceFlightAPIClient
 */

using System.Net.Http.Json;
using Spaceflight;
using Spaceflight.Models;

var http = new HttpClient();
await using var articlesWriter =
    new FileWriter("/Users/Francesco/Documents/Ellycode/Spaceflight/Spaceflight/Articles.txt");

for (var i = 0; i < 10; i++)
{
    Console.Write($"Request articles N.{i + 1}\t");
    var httpResponseArticles = await http
        .GetAsync($"https://api.spaceflightnewsapi.net/v3/articles?_limit=10&_sort=publishedAt:desc&_start={i * 10}");
    if (httpResponseArticles.IsSuccessStatusCode)
    {
        Console.WriteLine("Server has responded for articles");
        var articles = await httpResponseArticles.Content.ReadFromJsonAsync<List<Article>>();
        if (articles is not null)
        {
            foreach (var article in articles)
            {
                await articlesWriter.WriteAsync(article);
            }
        }
    }
    else
    {
        Console.WriteLine("Server has not responded for articles");
    }
}

await using var blogsWriter =
    new FileWriter("/Users/Francesco/Documents/Ellycode/Spaceflight/Spaceflight/Blogs.txt");
for (var i = 0; i < 10; i++)
{
    Console.Write($"Request blogs N.{i + 1}\t");
    var httpResponseBlogs = await http
        .GetAsync($"https://api.spaceflightnewsapi.net/v3/blogs?_limit=10&_sort=publishedAt:desc&_start={i * 10}");
    if (httpResponseBlogs.IsSuccessStatusCode)
    {
        Console.WriteLine("Server has responded for blogs");
        var blogs = await httpResponseBlogs.Content.ReadFromJsonAsync<List<Blog>>();
        if (blogs is not null)
        {
            foreach (var blog in blogs)
            {
                await blogsWriter.WriteAsync(blog);
            }
        }
    }
    else
    {
        Console.WriteLine("Server has not responded for blogs");
    }
}