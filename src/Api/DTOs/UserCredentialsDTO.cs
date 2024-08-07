using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class UserCredentialsDTO
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        [Required]
        public bool Active { get; set; } 
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; }
    }
}