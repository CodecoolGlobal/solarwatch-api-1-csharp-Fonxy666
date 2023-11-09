using Microsoft.AspNetCore.Identity;

namespace SolarWatchMvp.Services.Authentication;

public interface ITokenService
{
    string CreateToken(IdentityUser user, string? role);
}