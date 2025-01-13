using System.ComponentModel.DataAnnotations;

namespace UsersService.Dtos
{
    public class LoginRequest
    {
        [EmailAddress]

        public string Email { get; set; }

        public string Password { get; set; }

    }
}
