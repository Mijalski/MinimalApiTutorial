using MinimalApiTutorial.Modules.Books.Entities;
using MinimalApiTutorial.Shared.Database;

namespace MinimalApiTutorial.Modules.Books.Commands;

class CreateHandler
{
    private readonly ApplicationDbContext _dbContext;

    public CreateHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task HandleAsync(CreateBookDto command, CancellationToken cancellationToken = default)
    {
        var book = new Book
        {
            CoverType = command.CoverType,
            Description = command.Description,
            Language = command.Language,
            PageCount = command.PageCount,
            PublishedOn = command.PublishedOn,
            Title = command.Title
        };

        await _dbContext.Set<Book>().AddAsync(book, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
