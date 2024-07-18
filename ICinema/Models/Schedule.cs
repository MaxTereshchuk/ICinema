using System.ComponentModel.DataAnnotations;

namespace ICinema.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public ICollection<Screaning> Screanings { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
    }
}
