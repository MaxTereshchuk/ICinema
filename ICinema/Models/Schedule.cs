using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ICinema.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public ICollection<Screaning>? Screanings { get; set; }
        [ForeignKey("Film")]
        public int FilmId { get; set; }
        [JsonIgnore]
        public Film? Film { get; set; }
        [NotMapped]
        public string Date { get { return Day.Date.ToString(); } }
        [NotMapped]
        public string Time { get { return Day.TimeOfDay.ToString(); } }

    }
}
