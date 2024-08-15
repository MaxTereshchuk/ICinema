using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ICinema.Models
{
    public class Screaning
    {
        [Key]
        public int Id { get; set; }
        public DateTime Day{ get; set; }
        [ForeignKey("Schedule")]
        public int ScheduleId { get; set; }
        [JsonIgnore]
        public Schedule Schedule { get; set; }

        [ForeignKey("Hall")]
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

    }
}
