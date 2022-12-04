using Microsoft.EntityFrameworkCore;
using MinimalApiTutorial.Modules.Books.Entities;
using MinimalApiTutorial.Shared.Database;

namespace MinimalApiTutorial.Modules.Books.Queries;

class GetAll
{
    private readonly DbSet<Book> _books;

    public GetAll(ApplicationDbContext dbContext)
    {
        _books = dbContext.Set<Book>() ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<BookDto>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var books = await _books.ToListAsync(cancellationToken);

        return books.Select(x => new BookDto
        {
            CoverType = x.CoverType,
            Description = x.Description,
            Id = x.Id,
            Language = x.Language,
            PageCount = x.PageCount,
            PublishedOn = x.PublishedOn,
            Title = x.Title
        });
    }
}
