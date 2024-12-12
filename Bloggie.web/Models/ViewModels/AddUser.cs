﻿using System.ComponentModel.DataAnnotations;

namespace Bloggie.web.Models.ViewModels
{
    public class AddUser
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public bool AdminCheckBox { get; set; }
    }
}
