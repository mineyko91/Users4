using System;

namespace _4zadanie.ViewModels
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Selected { get; set; }

        public bool IsEnabled { get; set; }

        public DateTimeOffset LastLoginTime { get; set; }

        public DateTimeOffset RegistrationDate { get; set; }
    }
}
