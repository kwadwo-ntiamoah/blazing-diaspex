using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class AddPostDto
    {
        [Required(ErrorMessage = "CategoryId is required")]
        public Guid CategoryId {get; set;}
        [Required(ErrorMessage = "Title is required")]
        public string? Title {get; set;}
        [Required(ErrorMessage = "Content is required")]
        public string? Content {get; set;}
        [Required(ErrorMessage = "PostType is required")]
        public PostType Type {get; set;}
    }

    public class EditPostDto {
        [Required(ErrorMessage = "Title is required")]
        public string? Title {get; set;}
        [Required(ErrorMessage = "Content is required")]
        public string? Content {get; set;}
    }
}