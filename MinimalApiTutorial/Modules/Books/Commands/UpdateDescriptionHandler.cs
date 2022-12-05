using Microsoft.EntityFrameworkCore;
using MinimalApiTutorial.Modules.Books.Entities;
using MinimalApiTutorial.Shared.Database;

namespace MinimalApiTutorial.Modules.Books.Commands;

class UpdateDescriptionHandler
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateDescriptionHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task HandleAsync(UpdateDescriptionDto command, CancellationToken cancellationToken = default)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (book is null)
        {
            return;
        }

        book.Description = command.Description;

        //_dbContext.Set<Book>().Update(book);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
