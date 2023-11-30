using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class AddReplyDto
    {
        [Required(ErrorMessage = "PostId is required")]
        public Guid PostId { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string? Content { get; set; } 
    }
}