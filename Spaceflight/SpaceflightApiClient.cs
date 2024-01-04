using System.Net.Http.Json;
using Spaceflight.Models;

namespace Spaceflight;

public class SpaceflightApiClient(HttpClient httpClient)
{
    private const int ElementsPerPage = 10;
    private static readonly Uri BaseAddress = new UriBuilder("https://api.spaceflightnewsapi.net/v3").Uri;

    public async Task<List<Article>> GetArticlesAsync(int totalArticles)
    {
        return await GetWithPaginationAsync<Article>(totalArticles);
    }

    public async Task<List<Blog>> GetBlogsAsync(int totalBlogs)
    {
        return await GetWithPaginationAsync<Blog>(totalBlogs);
    }

    private async Task<List<T>?> GetAsync<T>(Uri uri) where T : IEntity
    {
        var httpResponseMessage = await httpClient.GetAsync(uri);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            Console.WriteLine($"Server has responded for {typeof(T).Name}");
            return await httpResponseMessage.Content.ReadFromJsonAsync<List<T>?>();
        }

        Console.WriteLine($"Server has not responded for {typeof(T).Name}");
        return null;
    }

    private async Task<List<T>> GetWithPaginationAsync<T>(int total) where T : IEntity
    {
        var result = new List<T>();
        var numberOfPages = Math.Ceiling((double)total / ElementsPerPage);
        var itemsType = typeof(T).Name.ToLower() + "s";
        for (var currentPage = 1; currentPage <= numberOfPages; currentPage++)
        {
            // if (currentPage == numberOfPages)
            // {
            //     _elementsPerPage = total - result.Count;
            // }
            Console.Write($"Request {typeof(T).Name} page: {currentPage}\t\t");
            var skip = (currentPage - 1) * ElementsPerPage;
            var elementsToRequest = Math.Min(ElementsPerPage, total - skip);
            var uri = new Uri(
                $"{BaseAddress}/{itemsType}?_limit={elementsToRequest}&_sort=publishedAt:desc&_start={skip}");
            var items = await GetAsync<T>(uri);
            if (items is not null)
            {
                result.AddRange(items);
            }
            else
            {
                throw new Exception("Error while getting data");
            }
        }

        return result;
    }
}