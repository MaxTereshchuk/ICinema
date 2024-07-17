using ICinema.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet;

namespace ICinema.ViewModels
{
    public class CreateTicketVM
    {
        [Required]
        [Display(Name = "Movie Name")]
		public string MovieName { get; set; }
		[Required]
		[Display(Name = "Date")]
		public DateTime Date { get; set; }
		[Required]
		[Display(Name = "ScreaningId")]
		public int ScreaningId { get; set; }
		[Required]
		[Display(Name = "Number of Row")]
		public int RowNumber { get; set; }
		[Required]
		[Display(Name = "Number of Seat")]
		public int SeatNumber { get; set; }
		[Required]
		[Display(Name = "Price")]
		public decimal Price { get; set; }
		[Required]
		[Url]
		[Display(Name = "ImageUrl")]
		[DataType(DataType.Url)]
		public string ImageUrl { get; set; }

	}
}
