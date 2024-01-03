using Spaceflight.Models;

namespace Spaceflight;

public class FileWriter(string path) : IDisposable, IAsyncDisposable
{
    private readonly StreamWriter _writer = new(path);

    public async Task WriteAsync<T>(T entity) where T : class, IEntity
    {
        var publishedAt = entity.PublishedAt is null
            ? ""
            : DateTime.Parse(entity.PublishedAt).ToString("dd/MM/yyyy");
        await _writer.WriteLineAsync(
            $"{entity.Id} | {entity.Title} | {publishedAt}");
    }

    public void Dispose()
    {
        _writer.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _writer.DisposeAsync();
    }
}