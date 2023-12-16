﻿using System.ComponentModel.DataAnnotations;

namespace StudyTimer.MVC.Models.Auth
{
    public class AuthLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}