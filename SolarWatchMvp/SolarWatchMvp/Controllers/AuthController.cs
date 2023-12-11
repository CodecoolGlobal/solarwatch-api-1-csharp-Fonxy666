using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Contracts;
using SolarWatchMvp.Data;
using SolarWatchMvp.Services.Authentication;

namespace SolarWatchMvp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    private readonly UsersContext _repository;
    private readonly ILogger<AuthController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(IAuthService authenticationService, UsersContext repository, ILogger<AuthController> logger, UserManager<IdentityUser> userManager)
    {
        _authenticationService = authenticationService;
        _repository = repository;
        _logger = logger;
        _userManager = userManager;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.RegisterAsync(request.Email, request.Username, request.Password, "User");

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
    }

    private void AddErrors(AuthResult result)
    {
        foreach (var resultErrorMessage in result.ErrorMessages)
        {
            ModelState.AddModelError(resultErrorMessage.Key, resultErrorMessage.Value);
        }
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.LoginAsync(request.UserName, request.Password);

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return Ok(new AuthResponse(result.Email, result.UserName, result.Token));
    }

    [HttpPatch("Put"), Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<ChangeUserPasswordResponse>> ChangeUserPassword([FromBody] ChangeUserPasswordRequest request)
    {
        try
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
            {
                _logger.LogInformation($"Data for email: {request.Email} doesnt't exists in the database.");
                return Ok(existingUser);
            }

            var result = await _userManager.ChangePasswordAsync(existingUser, request.OldPassword, request.NewPassword);

            await _repository.SaveChangesAsync();

            if (result.Succeeded)
            {
                await _repository.SaveChangesAsync();
                return Ok($"Successful update on {request.Email}!");
            }
            else
            {
                _logger.LogError($"Error changing password for user {request.Email}: {string.Join(", ", result.Errors)}");
                return BadRequest($"Error changing password for user {request.Email}");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error delete sun data");
            return NotFound("Error delete sun data");
        }
    }
}