using System.ComponentModel.DataAnnotations;

namespace ICinema.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Hall { get; set; }
    }
}
