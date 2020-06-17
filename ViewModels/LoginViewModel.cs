using System;
using System.ComponentModel.DataAnnotations;

namespace _4zadanie.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public DateTimeOffset LastLoginTime { get; set; }

        public DateTimeOffset RegistrationDate { get; set; }
    }
}
