using MinimalApiTutorial.Modules.Users.Entities;

namespace MinimalApiTutorial.Shared.Users;

interface ICurrentUserService
{
    Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}
