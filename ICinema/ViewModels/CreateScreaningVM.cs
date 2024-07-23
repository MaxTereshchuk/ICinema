using ICinema.Models;
namespace ICinema.ViewModels
{
    public class CreateScreaningVM
    {
        public DateTime Day { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }


        public int HallId { get; set; }
        public Hall Hall { get; set; }
    }
}
