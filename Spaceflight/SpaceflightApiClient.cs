using System.Net.Http.Json;
using Spaceflight.Models;

namespace Spaceflight;

public class SpaceflightApiClient(HttpClient httpClient)
{
    public async Task<List<T>?> GetAsync<T>(string path) where T: class, IEntity
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
}