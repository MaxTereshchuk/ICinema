using System.ComponentModel.DataAnnotations;

namespace ICinema.ViewModels
{
	public class RegisterVM
	{
		[Required]
		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[Display(Name = "Confirm the Password")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage= "Password do not match")]
		public string ConfirmPassword { get; set; }

		[Display(Name="Admin")]
		public bool IsAdmin { get; set; }
	}
}
