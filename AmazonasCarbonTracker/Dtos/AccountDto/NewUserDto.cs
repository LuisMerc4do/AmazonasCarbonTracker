using System.ComponentModel.DataAnnotations;

namespace AmazonasCarbonTracker.Dtos
{
    public class NewUserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
