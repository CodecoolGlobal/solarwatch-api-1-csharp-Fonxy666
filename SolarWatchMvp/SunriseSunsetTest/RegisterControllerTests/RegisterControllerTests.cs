﻿/*
using Microsoft.AspNetCore.Mvc;
using Moq;
using SolarWatchMvp.Contracts;
using SolarWatchMvp.Controllers;
using SolarWatchMvp.Services.Authentication;

namespace SunriseSunsetTest.RegisterControllerTests;

[TestFixture]
public class AuthControllerTests
{
    private AuthController _authController;
    private Mock<IAuthService> _mockAuthService;

    [SetUp]
    public void Setup()
    {
        _mockAuthService = new Mock<IAuthService>();
        _authController = new AuthController(_mockAuthService.Object);
    }

    [Test]
    public async Task Register_ValidInput_ReturnsCreatedResult()
    {
        var registrationRequest = new RegistrationRequest("test@example.com", "testuser", "testpassword");

        var registrationResponse = new RegistrationResponse(registrationRequest.Email, registrationRequest.Username);
        var authResult = new AuthResult(true, registrationRequest.Email, registrationRequest.Username, "");

        _mockAuthService.Setup(service => service.RegisterAsync(
            registrationRequest.Email, 
            registrationRequest.Username, 
            registrationRequest.Password, 
            "User")).ReturnsAsync(authResult);
        
        var result = await _authController.Register(registrationRequest);
        
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = (CreatedAtActionResult)result.Result;
        Assert.AreEqual("Register", createdResult.ActionName);
        var data = (RegistrationResponse)createdResult.Value;
        Assert.AreEqual(registrationResponse.Email, data.Email);
        Assert.AreEqual(registrationResponse.UserName, data.UserName);
    }

    [Test]
    public async Task Authenticate_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var authRequest = new AuthRequest("testuser", "testpassword");

        var authResponse = new AuthResponse(authRequest.UserName, "test@example.com", "testtoken");
        var authResult = new AuthResult(true, authRequest.UserName, "test@example.com", "testtoken");

        _mockAuthService.Setup(service => service.LoginAsync(authRequest.UserName, authRequest.Password)).ReturnsAsync(authResult);
        
        var result = await _authController.Authenticate(authRequest);
        
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = (OkObjectResult)result.Result;
        var data = (AuthResponse)okResult.Value;
        Assert.AreEqual(authResponse.Username, data.Username);
        Assert.AreEqual(authResponse.Email, data.Email);
        Assert.AreEqual(authResponse.Token, data.Token);
    }
}
*/
