using ICinema.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ICinema.Data
{
    public class AppDBContext:IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options ): base(options) 
        {
        
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<EmailSettings>EmailSettings { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Screaning> Screanings { get; set; }
        public DbSet<Hall> Halls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Screaning)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.ScreaningId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AppUser)
                .WithMany(u => u.MyTickets)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Restrict); 
        }


    }
}
