using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICinema.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Screaning")]
        public int ScreaningId { get; set; }
        //public Screaning Screaning{ get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }


    }
}
