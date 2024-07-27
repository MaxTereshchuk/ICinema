using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ICinema.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Screaning")]
        public int ScreaningId { get; set; }
        public Screaning Screaning { get; set; }
        
        public List<Ticket> Tickets { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
