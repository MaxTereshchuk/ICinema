using Microsoft.AspNetCore.Identity;
namespace ICinema.Models
{
    public class AppUser: IdentityUser
    {
        public string? PhoneNumber {  get; set; }
        public decimal Balance { get; set; }
        public string? CardInfo  { get; set; }
        public ICollection<Ticket> MyTickets { get; set; }
    }
}
