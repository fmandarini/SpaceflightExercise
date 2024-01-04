using System.Net.Http.Json;
using Spaceflight.Models;

namespace Spaceflight;

public class SpaceflightApiClient(HttpClient httpClient)
{
    private async Task<List<T>?> GetAsync<T>(string path) where T : IEntity
    {
        var httpResponseMessage = await httpClient.GetAsync(path);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            Console.WriteLine($"Server has responded for {typeof(T).Name}");
            return await httpResponseMessage.Content.ReadFromJsonAsync<List<T>?>();
        }

        Console.WriteLine($"Server has not responded for {typeof(T).Name}");
        return null;
    }

    public async Task<List<Article>> GetArticlesAsync()
    {
        var result = new List<Article>();
        for (var i = 0; i < 10; i++)
        {
            Console.Write($"Request articles N.{i + 1}\t");
            var articles =
                await GetAsync<Article>(
                    $"https://api.spaceflightnewsapi.net/v3/articles?_limit=10&_sort=publishedAt:desc&_start={i * 10}");
            if (articles is not null)
            {
                result.AddRange(articles);
            }
            else
            {
                throw new Exception("Error while getting data");
            }
        }

        return result;
    }
}