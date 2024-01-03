/* TODO - Per domani
 1. Rendere la casse file writer generica
 2. Spostare i metodi per la lettura di articoli e blog in una
    nuova classe SpaceFlightAPIClient
 3. Rendere i path relativi
 4. (Bonus) Creare degli overload asyncEnumerable dei metodi di SpaceFlightAPIClient
 */

using Spaceflight;
using Spaceflight.Models;

var spaceflightClient = new SpaceflightApiClient(new HttpClient());

await using var articlesWriter =
    new FileWriter("/Users/Francesco/Documents/Ellycode/Spaceflight/Spaceflight/Articles.txt");
for (var i = 0; i < 10; i++)
{
    Console.Write($"Request articles N.{i + 1}\t");
    var articles = await spaceflightClient
        .GetAsync<Article>(
            $"https://api.spaceflightnewsapi.net/v3/articles?_limit=10&_sort=publishedAt:desc&_start={i * 10}");
    if (articles is not null)
    {
        foreach (var article in articles)
        {
            await articlesWriter.WriteAsync(article);
        }
    }
    else
    {
        throw new NullReferenceException("No items were returned");
    }
}

await using var blogsWriter =
    new FileWriter("/Users/Francesco/Documents/Ellycode/Spaceflight/Spaceflight/Blogs.txt");
for (var i = 0; i < 10; i++)
{
    Console.Write($"Request blogs N.{i + 1}\t");
    var blogs = await spaceflightClient
        .GetAsync<Blog>($"https://api.spaceflightnewsapi.net/v3/blogs?_limit=10&_sort=publishedAt:desc&_start={i * 10}");
    if (blogs is not null)
    {
        foreach (var blog in blogs)
        {
            await blogsWriter.WriteAsync(blog);
        }
    }
    else
    {
        throw new NullReferenceException("No items were returned");
    }
}