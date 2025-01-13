using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersService.Models
{
    public class TeamMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TeamId { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        [Required]

        public int UserId { get; set; }

        [ForeignKey("TeamId")]
        public User User { get; set; }

        public string Role { get; set; }
    }
}
