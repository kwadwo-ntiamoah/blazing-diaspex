namespace Data.Models
{
    public class Post
    {
        public Guid Id {get; set;}
        public Guid CategoryId { get; set; }
        public string? Owner { get; set; }
        public string? Title { get; set; } 
        public string? Content { get; set; }
        public bool IsDeleted { get; set; }
        public PostType Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}