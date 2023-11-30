using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class AddNewsDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string? Title {get; set;}
        [Required(ErrorMessage = "Content is required")]
        public string? Content {get; set;} 
        public string? Image {get; set;} 
    }
}