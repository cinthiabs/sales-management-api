using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class UserCredentialsDto
    {
        [Required]
        public string Username { get; set; } 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public bool? Active { get; set; } 
        [Required]
        public DateTime? LastLogin { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpiration { get; set; }
    }
}