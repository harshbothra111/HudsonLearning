﻿using System.ComponentModel.DataAnnotations;

namespace HudsonLearning.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
        public IFormFile ProfilePhoto { get; set; }
    }
}