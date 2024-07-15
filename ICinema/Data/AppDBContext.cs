using ICinema.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ICinema.Data
{
    public class AppDBContext:IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options ): base(options) 
        {
        
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Card> Cards { get; set; }

        public DbSet<Film> Films { get; set; }



    }
}
