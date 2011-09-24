using System.ComponentModel.DataAnnotations;

namespace LearnMVC.Models
{
    [MetadataType(typeof(LogOnModelValidation))]
    public class LogOnModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LogOnModelValidation
    {
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(LearnMVCResources))]
        // TODO: Write T4 template that generates class with resource rows name
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}