using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MinimalApiTutorial.Modules.Users.Entities;
using MinimalApiTutorial.Shared.Database;

namespace MinimalApiTutorial.Shared.Users;

class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _dbContext;

    public CurrentUserService(IHttpContextAccessor accessor,
        ApplicationDbContext dbContext)
    {
        _httpContextAccessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var name = _httpContextAccessor.HttpContext?.User.FindFirstValue("Name");
        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidOperationException("User is not logged into application!");
        }

        return await _dbContext.Set<User>().SingleAsync(a => a.Name == name, cancellationToken);
    }
}
