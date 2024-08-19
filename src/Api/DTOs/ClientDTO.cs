using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class ClientDto
    {
        public int? Id {get; set;}
        [Required]
        public string Name {get;set;} = default!;
        public string? Phone {get;set;}
        public string? Location {get;set;}
        public bool Active { get; set; } = true;
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
    }
}