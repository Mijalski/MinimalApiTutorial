using Microsoft.AspNetCore.Identity;

namespace MinimalApiTutorial.Modules.Users.Entities;

class User : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
}
