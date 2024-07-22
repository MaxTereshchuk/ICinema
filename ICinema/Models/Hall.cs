using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet.Actions;

namespace ICinema.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        
        public string SeatsJson { get; set; }

        public ICollection<Screaning> Screanings { get; set; }
    }
}
