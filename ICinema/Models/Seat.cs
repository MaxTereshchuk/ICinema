using System.ComponentModel.DataAnnotations;
namespace ICinema.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public bool _IsOccupied { get; set; }
        public bool _IsColumn {  get; set; }
    }
}
