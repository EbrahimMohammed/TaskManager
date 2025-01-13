using System.ComponentModel.DataAnnotations;

namespace UsersService.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

    }
}
