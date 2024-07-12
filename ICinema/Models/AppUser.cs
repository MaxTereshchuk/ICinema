using Microsoft.AspNetCore.Identity;
namespace ICinema.Models
{
    public class AppUser: IdentityUser
    {
       
        public decimal Balance { get; set; }
        
        public ICollection<Ticket> MyTickets { get; set; }

        public Card? Card { get; set; }
    }
}
