using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class Tokens
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class TokenDto
    {
        public string? UserId { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}