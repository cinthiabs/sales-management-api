namespace Domain.Dtos
{
    public class AddressDto
    {
        public string ZipCode { get; set; } = default!;
        public string? Street { get; set; }
        public string? Complement { get; set; }
        public string? Unit { get; set; }
        public string? Neighborhood { get; set; }
        public string City { get; set; } = default!;
        public string Uf { get; set; } = default!;
        public string State { get; set; } = default!;
        public string? Region { get; set; }
        public string? IbgeCode { get; set; }
        public string? GiaCode { get; set; }
    }
}
