using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    // for username and password
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
