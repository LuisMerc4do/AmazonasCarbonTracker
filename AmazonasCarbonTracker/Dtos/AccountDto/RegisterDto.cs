using System.ComponentModel.DataAnnotations;

namespace AmazonasCarbonTracker.Dtos.AccountDto;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    public string UserName { get; set; }

}
