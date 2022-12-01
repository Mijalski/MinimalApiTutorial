using Microsoft.EntityFrameworkCore;
using MinimalApiTutorial.Modules.Users.Entities;
using MinimalApiTutorial.Modules.Users.Jwts;
using MinimalApiTutorial.Modules.Users.Passwords;
using MinimalApiTutorial.Shared.Database;

namespace MinimalApiTutorial.Modules.Users.Commands;

class LogInHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserPasswordService _userPasswordService;
    private readonly IJwtTokenGeneratorService _tokenGeneratorService;

    public LogInHandler(ApplicationDbContext dbContext,
        IUserPasswordService userPasswordService,
        IJwtTokenGeneratorService tokenGeneratorService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userPasswordService = userPasswordService ?? throw new ArgumentNullException(nameof(userPasswordService));
        _tokenGeneratorService = tokenGeneratorService ?? throw new ArgumentNullException(nameof(tokenGeneratorService));
    }

    public async Task<string?> HandleAsync(LogInDto command, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Set<User>()
            .SingleOrDefaultAsync(a => a.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var isValid = _userPasswordService.VerifyPasswordHash(user, command.Password);

        return isValid
            ? await _tokenGeneratorService.GenerateTokenAsync(user, cancellationToken)
            : null;
    }
}
