using Microsoft.AspNetCore.Identity;

namespace SolarWatchMvp.Services;

public interface ITokenService
{
    string CreateToken(IdentityUser user, string? role);
}