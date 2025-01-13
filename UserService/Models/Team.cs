﻿using System.ComponentModel.DataAnnotations;

namespace UsersService.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
