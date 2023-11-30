using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class SignUpDto
    {
        public SignUpAuthDto? Auth { get; set; }
        public SignUpProfileDto? Profile { get; set; }
    }

    public class SignUpAuthDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

    public class SignUpProfileDto
    {
        [Required(ErrorMessage = "Surname is required")]
        public string? Surname { get; set; }
        [Required(ErrorMessage = "Othernames is required")]
        public string? OtherNames { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}