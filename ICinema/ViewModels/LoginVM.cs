using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace ICinema.ViewModels
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
