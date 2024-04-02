using System.ComponentModel.DataAnnotations;
namespace HotelListning.Models.User
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Password Should be within  {2} , {1}", MinimumLength = 4)]
        public string Password { get; set; }
    }
}

