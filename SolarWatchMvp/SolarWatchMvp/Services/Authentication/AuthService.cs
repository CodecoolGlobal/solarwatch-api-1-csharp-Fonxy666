using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace SolarWatchMvp.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenService _tokenService;
    
    public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }


    public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role)
    {
        var user = new IdentityUser { UserName = username, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return FailedRegistration(result, email, username);
        }

        await _userManager.AddToRoleAsync(user, role);
        return new AuthResult(true, email, username, "");
    }

    private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
    {
        var authenticationResult = new AuthResult(false, email, username, "");

        foreach (var identityError in result.Errors)
        {
            authenticationResult.ErrorMessages.Add(identityError.Code, identityError.Description);
        }

        return authenticationResult;
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var managedUser = await _userManager.FindByNameAsync(username);

        if (managedUser == null)
        {
            return InvalidLogin();
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);

        if (!isPasswordValid)
        {
            return InvalidLogin();
        }

        var roles = await _userManager.GetRolesAsync(managedUser);
        var accessToken = _tokenService.CreateToken(managedUser, roles[0]);

        return new AuthResult(true, managedUser.Email!, managedUser.UserName!, accessToken);
    }

    private AuthResult InvalidLogin()
    {
        var result = new AuthResult(false, "", "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid email");
        return result;
    }
}