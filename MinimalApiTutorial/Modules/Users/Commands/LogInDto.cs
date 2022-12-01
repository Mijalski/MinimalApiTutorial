namespace MinimalApiTutorial.Modules.Users.Commands;

public class LogInDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
