using MinimalApiTutorial.Modules.Users.Entities;

namespace MinimalApiTutorial.Modules.Users.Passwords;

interface IUserPasswordService
{
    string CreatePasswordHash(string input);
    bool VerifyPasswordHash(User user, string input);
}
