using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Username {get;set;} = default!;
        public string Email { get; set; } = default!;
        public string? Name { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        public string? ConfirmPassword { get; set; } = default!;
    }
}