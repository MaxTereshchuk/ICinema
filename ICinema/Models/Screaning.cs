using System.ComponentModel.DataAnnotations;

namespace ICinema.Models
{
    public class Screaning
    {
        [Key]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

    }
}
