using System.ComponentModel.DataAnnotations;

namespace ICinema.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
