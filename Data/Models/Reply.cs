namespace Data.Models
{
    public class Reply
    {
        public Guid Id { get; set; }
        public string? Owner { get; set; }
        public Guid PostId { get; set; }
        public string? Content { get; set; } 
        public bool IsDeleted {get; set;}
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}