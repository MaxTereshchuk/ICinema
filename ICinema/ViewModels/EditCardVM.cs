using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace ICinema.ViewModels
{
	public class EditCardVM
	{
		[Required]
		[Display(Name ="Card Number")]
		[DataType(DataType.CreditCard)]
		public string CardNumber	{ get; set; }
		[Required]
		[Display(Name = "Card Holder Name")]
		public string CardHolderName { get; set; }
		[Required]
		[Display(Name = "Expiry Date")]
		[DataType(DataType.DateTime)]
		public DateTime ExpiryDate { get; set; }
		[Required]
		[Display(Name = "CVV")]
		[Range(100, 9999, ErrorMessage = "CVV must be a 3 or 4 digit number.")]
		public int CVV { get; set; }
	}
}
