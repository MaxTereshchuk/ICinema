using ICinema.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICinema.ViewModels
{
    public class CreateScheduleVM
    {

        [Required]
        [Display(Name ="Date")]
        [DataType(DataType.DateTime)]
        public DateTime Day { get; set; }     
        public int FilmId { get; set; }
        public Film Film { get; set; }

		[Required]
		[Display(Name = "Number of Screenings")]
		
		public int NumberOfScreanings { get; set; }
        
    }
}
