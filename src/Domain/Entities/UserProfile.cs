namespace Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? Image { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public string? Address { get; set; } = default!;
        public string? City { get; set; } = default!;
        public string? State { get; set; } = default!;
        public string? ZipCode { get; set; } = default!;
        public int AccessLevelId { get; set; } = default!;
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
    }
}
