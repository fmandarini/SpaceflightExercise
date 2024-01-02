/* TODO - Code changing
 1. Read 100 most recent articles (read 10 at time)
 2. Write articles in a file (id | title | publicationDate (GG/MM/YYYY) )
 */

using System.Net.Http.Json;
using Spaceflight.Models;

var http = new HttpClient();
await using var streamWriterArticles =
    new StreamWriter("/Users/Francesco/Documents/Ellycode/Spaceflight/Spaceflight/Articles.txt");

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
                var publishedAt = article.PublishedAt is null
                    ? ""
                    : DateTime.Parse(article.PublishedAt).ToString("dd/MM/yyyy");
                await streamWriterArticles.WriteLineAsync(
                    $"{article.Id} | {article.Title} | {publishedAt}");
            }
        }
    }
    else
    {
        Console.WriteLine("Server has not responded for articles");
    }
}

await using var streamWriterBlogs =
    new StreamWriter("/Users/Francesco/Documents/Ellycode/Spaceflight/Spaceflight/Blogs.txt");
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
                var publishedAt = blog.PublishedAt is null
                    ? ""
                    : DateTime.Parse(blog.PublishedAt).ToString("dd/MM/yyyy");
                await streamWriterBlogs.WriteLineAsync(
                    $"{blog.Id} | {blog.Title} | {publishedAt}");
            }
        }
    }
    else
    {
        Console.WriteLine("Server has not responded for blogs");
    }
}