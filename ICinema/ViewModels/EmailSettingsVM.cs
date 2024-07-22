using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ICinema.ViewModels
{
    public class EmailSettingsVM
    {

        [Required]
        [Display(Name ="StmpUsername")]
        [DataType(DataType.EmailAddress)]
        public string SmtpUsername { get; set; }
        [Required]
        [Display(Name = "SmtpPassword")]
        [DataType(DataType.Password)]
        public string SmtpPassword { get; set; }
        [Required]
        [Display(Name = "StmpUsername")]
        [DataType(DataType.EmailAddress)]
        public string SenderEmail { get; set; }
        
    }
}
