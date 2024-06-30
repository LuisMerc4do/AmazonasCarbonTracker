using System.ComponentModel.DataAnnotations;

namespace AmazonasCarbonTracker.Dtos.AccountDto;

public class LoginDto
{

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

}
