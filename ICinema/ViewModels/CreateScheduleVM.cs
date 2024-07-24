using ICinema.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICinema.ViewModels
{
    public class CreateScheduleVM
    {

        
        public DateTime Day { get; set; }     
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public string Date { get; set; }
        
        public int NumberOfScreanings { get; set; }
        
    }
}
