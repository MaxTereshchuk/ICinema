using System.ComponentModel.DataAnnotations;

namespace ICinema.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        public int[] Matrix { get; set; }
    }
}
