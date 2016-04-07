using System.ComponentModel.DataAnnotations;

namespace Goofy.Component.Auth.Models
{
    public class Register
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required]
        [MaxLength(100)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
