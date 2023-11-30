using System.ComponentModel.DataAnnotations;
using Data.Enums;

namespace Data.DTOs
{
    public class AddCategoryDto {
        [Required(ErrorMessage = "Category Type is required")]
        public CategoryType CategoryType {get; set;}
        [Required(ErrorMessage = "Title is required")]
        public string? Title {get; set;}
        [Required(ErrorMessage = "Description")]
        public string? Description {get; set;}
    }

    public class EditCategoryDto {
        [Required(ErrorMessage = "Title is required")]
        public string? Title {get; set;}
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
    }
}