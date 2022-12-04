namespace MinimalApiTutorial.Modules.Books.Queries;

class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset PublishedOn { get; set; }
    public string Language { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public string CoverType { get; set; } = string.Empty;
}