using System.ComponentModel.DataAnnotations;
using ICinema.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ICinema.ViewModels
{
    public class CreateScreaningVM
    {
        [Required]
        [Display(Name ="Date")]
        [DataType(DataType.DateTime)]
        public DateTime Day { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }


        public int HallId { get; set; }
        public Hall Hall { get; set; }
		[Required]
		[Display(Name = "Available Halls")]
		
		public ICollection<SelectListItem> AvailableHalls { get; set; }
	}
}
