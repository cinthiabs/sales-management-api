using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dtos
{
    public class UserCredentialsDto
    {
        [JsonIgnore]
        public int? Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public bool? Active { get; set; } 
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpiration { get; set; }
    }
}