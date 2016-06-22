﻿
using System.ComponentModel.DataAnnotations;

namespace Goofy.Presentation.Configuration.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}