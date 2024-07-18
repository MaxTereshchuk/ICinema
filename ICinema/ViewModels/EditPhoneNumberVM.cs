using System.ComponentModel.DataAnnotations;

namespace ICinema.ViewModels
{
	public class EditPhoneNumberVM
	{
		[Required]
		[DataType(DataType.PhoneNumber)]
		[Display(Name ="Phone Number")]
		public string PhoneNumber { get; set; }
	}
}
