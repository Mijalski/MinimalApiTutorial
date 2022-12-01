using MinimalApiTutorial.Modules.Users.Entities;
using MinimalApiTutorial.Modules.Users.Passwords;
using MinimalApiTutorial.Shared.Database;

namespace MinimalApiTutorial.Modules.Users.Commands;

class CreateHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserPasswordService _userPasswordService;

    public CreateHandler(ApplicationDbContext dbContext,
        IUserPasswordService userPasswordService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userPasswordService = userPasswordService ?? throw new ArgumentNullException(nameof(userPasswordService));
    }

    public async Task HandleAsync(CreateUserDto command, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Email = command.Email,
            Name = command.Name,
            PasswordHash = _userPasswordService.CreatePasswordHash(command.Password)
        };

        await _dbContext.Set<User>().AddAsync(user, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
