using Microsoft.EntityFrameworkCore;
using MinimalApiTutorial.Modules.Users.Entities;
using MinimalApiTutorial.Shared.Database;
using MinimalApiTutorial.Shared.Users;

namespace MinimalApiTutorial.Modules.Users.Commands;

class DeleteHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public DeleteHandler(ApplicationDbContext dbContext,
        ICurrentUserService currentUserService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public async Task HandleAsync(DeleteUserDto deleteUser, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Set<User>()
            .SingleAsync(a => a.Id == deleteUser.Id, cancellationToken);

        _dbContext.Set<User>().Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
