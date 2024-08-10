
namespace Domain.Entities
{
    public class Login
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Name { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? ConfirmPassword { get; set; } = default!;
    }
}
