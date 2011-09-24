using System;
using System.ComponentModel.DataAnnotations;

namespace LearnMVC.Models.Account
{
    public class UserProfile
    {
        public UserProfile(string name)
        {
            Login = name;
            DisplayName = name;
            BirthDate = DateTime.Now;
        }

        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Display name")]
        public string DisplayName { get; set; }

        public string Login { get; set; }
    }
}