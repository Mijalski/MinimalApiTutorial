using Microsoft.EntityFrameworkCore;
using MinimalApiTutorial.Modules.Users.Entities;
using MinimalApiTutorial.Shared.Database;

namespace MinimalApiTutorial.Modules.Users.Queries;

class GetByNameHandler
{
    private readonly DbSet<User> _users;

    public GetByNameHandler(ApplicationDbContext dbContext)
    {
        _users = dbContext.Set<User>() ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<UserDto?> GetUserOrDefault(string name, CancellationToken cancellationToken = default)
    {
        var dbUser = await _users.SingleOrDefaultAsync(a => a.Name == name, cancellationToken);

        if (dbUser is null)
        {
            return null;
        }

        return new UserDto
        {
            Email = dbUser.Email!,
            Name = dbUser.Name
        };
    }
}
