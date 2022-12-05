namespace MinimalApiTutorial.Modules.Books.Commands;

class UpdateDescriptionDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
}