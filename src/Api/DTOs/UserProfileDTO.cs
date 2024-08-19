using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
        [Required]
        public string AccessLevel { get; set; } = default!;
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
    }
}