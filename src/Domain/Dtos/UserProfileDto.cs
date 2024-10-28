using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class UserProfileDto
    {
        [Required]
        public string Username { get; set; } = default!;
        public string? Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; } 
        public string? Address { get; set; } 
        public string? City { get; set; }
        public string? Neighborhood { get; set; }
        public string? Number { get; set; }
        public string? State { get; set; } 
        public string? ZipCode { get; set; }
        [Required]
        public int AccessLevelId { get; set; } = default!;
    }
}