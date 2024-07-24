using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CloudinaryDotNet;

namespace ICinema.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public DateTime Time { get { return Screaning.Day; } }
        [NotMapped]
        public string Title { get { return Screaning.Schedule.Film.Title; } }

        [ForeignKey("Screaning")]
        public int ScreaningId {  get; set; }
        public Screaning Screaning { get; set; }
		public int RowNumber { get; set; }
        
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public string ImageUrl { get { return Screaning.Schedule.Film.Image; } }
		[ForeignKey("AppUser")]
		public string? AppUserId { get; set; }
		public AppUser AppUser { get; set; }
        public bool _isOccupied { get; set; }
        
	}
}
