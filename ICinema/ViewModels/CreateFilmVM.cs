using ICinema.Models;
using System.ComponentModel.DataAnnotations;

namespace ICinema.ViewModels
{
    public class CreateFilmVM
    {
        
        
        [Required]
        [Display(Name = "Title")]
        
        public string Title { get; set; }
        
        [Required]
        [Url]
        [Display(Name = "ImageUrl")]
        [DataType(DataType.Url)]
        public string Image { get; set; }
        
    }
}
