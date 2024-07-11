using Microsoft.AspNetCore.Identity;
namespace ICinema.Models
{
    public class AppUser: IdentityUser
    {
        
        ICollection<Ticket> Tickets { get; set; }
    }
}
