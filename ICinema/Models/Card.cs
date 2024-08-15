using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
namespace ICinema.Models
{
	public class Card
	{
		[Key]
		public int Id { get; set; }
		public string CardHolderName { get; set; }
		public string CardName { get; set; }
		
		public string CardNumber { get; set; }
		public DateTime ExpiryDate { get; set; }
		
		public int CVV { get; set; }

		[ForeignKey("AppUser")]
		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }


	}
}
