using System.ComponentModel.DataAnnotations;

namespace SolarWatchMvp.Contracts;

public record ChangeUserPasswordRequest([Required]string Email, [Required]string OldPassword, [Required]string NewPassword);