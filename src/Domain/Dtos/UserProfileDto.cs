using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class UserProfileDto
    {
        [Required]
        public string username { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
        [Required]
        public string AccessLevel { get; set; } = default!;
    }
}