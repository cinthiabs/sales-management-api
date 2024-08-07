using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        [Required]
        public string AccessLevel { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
    }
}