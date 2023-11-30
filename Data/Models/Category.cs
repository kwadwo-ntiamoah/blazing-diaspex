using Data.Enums;

namespace Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public CategoryType Type { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}