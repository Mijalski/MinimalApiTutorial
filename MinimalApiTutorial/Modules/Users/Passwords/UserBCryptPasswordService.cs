using MinimalApiTutorial.Modules.Users.Entities;

namespace MinimalApiTutorial.Modules.Users.Passwords;

class UserBCryptPasswordService : IUserPasswordService
{
    public string CreatePasswordHash(string input)
    {
        return BCrypt.Net.BCrypt.HashPassword(input);
    }

    public bool VerifyPasswordHash(User user, string input)
    {
        return BCrypt.Net.BCrypt.Verify(input, user.PasswordHash);
    }
}
