namespace Spaceflight.Models;

public interface IEntity
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string? ImageUrl { get; set; }
    public string? NewsSite { get; set; }
    public string? Summary { get; set; }
    public string? PublishedAt { get; set; }
}