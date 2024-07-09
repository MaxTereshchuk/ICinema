using Microsoft.AspNetCore.Identity;
namespace ICinema.Models
{
    public class AppUser: IdentityUser
    {
        public string Passport { get; set; }
        ICollection<Ticket> Tickets { get; set; }
    }
}
