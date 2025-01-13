using System.ComponentModel.DataAnnotations;

namespace UsersService.Dtos
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]

        public string Password { get; set; }
        [EmailAddress]

        public string Email { get; set; }
    }
}
