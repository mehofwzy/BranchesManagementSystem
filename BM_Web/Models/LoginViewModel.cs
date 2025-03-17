﻿using System.ComponentModel.DataAnnotations;

namespace BM_Web.Models
{
    public class LoginViewModel
    {
        [Required]
        //[EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
