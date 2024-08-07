namespace Domain.Entities
{
    public class UserCredentials
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string PasswordSalt { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Name { get; set; }
        public bool Active { get; set; } = true;
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Token { get; set; } = default!;
        public DateTime? TokenExpiration { get; set; }
    }
}
