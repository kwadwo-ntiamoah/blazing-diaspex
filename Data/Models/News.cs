namespace Data.Models
{
    public class News
    {
        public Guid Id {get; set;}
        public string? Title {get; set;}
        public string? Content {get; set;} 
        public string? Image {get; set;} 
        public DateTime CreatedDate {get; set;} = DateTime.Now;
        public DateTime ModifiedDate {get; set;} = DateTime.Now;
    }
}