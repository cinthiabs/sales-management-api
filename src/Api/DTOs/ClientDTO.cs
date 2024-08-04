using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class ClientDTO
    {
        public int Id {get; set;}
        [Required]
        public string Name {get;set;} = default!;
        public string? Phone {get;set;}
        public string? Location {get;set;}
        public bool Active { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
    }
}