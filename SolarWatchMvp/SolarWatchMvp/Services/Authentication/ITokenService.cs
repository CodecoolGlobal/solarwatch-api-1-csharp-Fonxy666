using Microsoft.AspNetCore.Identity;

namespace SolarWatchMvp.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user, string? role, bool isTest = false);
}