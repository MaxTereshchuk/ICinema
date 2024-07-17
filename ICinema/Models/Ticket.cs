using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CloudinaryDotNet;

namespace ICinema.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string MovieName { get; set; }
		public DateTime Date { get; set; }

		public int ScreaningId {  get; set; }
		public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
		public string ImageUrl { get; set; }
		[ForeignKey("AppUser")]
		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }


	}
}
