namespace Spaceflight.Models;

public class Article
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string? ImageUrl { get; set; }
    public string? NewsSite { get; set; }
    public string? Summary { get; set; }
    public string? PublishedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public bool Featured { get; set; }
}