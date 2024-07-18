using ICinema.Models;

namespace ICinema.ViewModels
{
	public class UserPersonalProfileVM
	{	
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public decimal Balance { get; set; }
		public Card Card{ get; set; }
		public ICollection<Ticket> MyTickets { get; set; }
	}
}
