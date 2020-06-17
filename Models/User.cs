using Microsoft.AspNetCore.Identity;
using System;

namespace _4zadanie.Models
{
    public class User : IdentityUser<int>
    {
        public DateTimeOffset LastLoginTime { get; set; }

        public DateTimeOffset RegistrationDate { get; set; }

        public bool IsEnabled { get; set; }
    }
}
