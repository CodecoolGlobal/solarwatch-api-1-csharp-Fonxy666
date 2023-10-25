using System.ComponentModel.DataAnnotations;

namespace SolarWatchMvp.Contracts;

public record RegistrationRequest([Required]string Email, [Required]string Username, [Required]string Password);