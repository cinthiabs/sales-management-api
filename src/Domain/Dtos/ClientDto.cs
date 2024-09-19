using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class ClientDto
    {
        [Required]
        public string Name {get;set;} = default!;
        public string? Phone {get;set;}
        public string? Location {get;set;}
        public bool Active { get; set; } = true;
    }
}