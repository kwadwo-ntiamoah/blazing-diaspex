namespace Data.Models
{
    public class Profile
    {
        public Guid Id {get; set;}
        public string? User { get; set; }
        public string? Surname { get; set; } 
        public string? OtherNames { get; set; } 
        public string? Country { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined {get; set;}
    }
}