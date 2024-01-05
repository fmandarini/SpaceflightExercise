using System.Net.Http.Json;
using Spaceflight.Models;

namespace Spaceflight;

public class SpaceflightApiClient
{
    private const int ElementsPerPage = 10;
    private readonly HttpClient _httpClient;
    public SpaceflightApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.spaceflightnewsapi.net/v3/");
    }

    public async Task<List<Article>> GetArticlesAsync(int totalArticles)
    {
        return await GetWithPaginationAsync<Article>(totalArticles, "articles");
    }

    public async Task<List<Blog>> GetBlogsAsync(int totalBlogs)
    {
        return await GetWithPaginationAsync<Blog>(totalBlogs, "blogs");
    }

    private async Task<List<T>?> GetAsync<T>(string uri) where T : IEntity
    {
        var httpResponseMessage = await _httpClient.GetAsync(uri);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Console.WriteLine($"Server has not responded for {uri}");
            return null;
        }

        Console.WriteLine($"Server has responded for {uri}");
        return await httpResponseMessage.Content.ReadFromJsonAsync<List<T>?>();
    }

    private async Task<List<T>> GetWithPaginationAsync<T>(int total, string path) where T : IEntity
    {
        var result = new List<T>();
        for (var readElements = 0; readElements < total;)
        {
            Console.Write($"{path} read: {readElements}\t\t");
            var elementsToRequest = Math.Min(ElementsPerPage, total - readElements);
            var items = await GetAsync<T>($"{path}?_limit={elementsToRequest}&_sort=publishedAt:desc&_start={readElements}");
            if (items is null)
            {
                throw new Exception("Error while getting data");
            }

            result.AddRange(items);
            readElements += items.Count;
        }

        return result;
    }
}