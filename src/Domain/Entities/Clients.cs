namespace Domain.Entities
{
    public class Clients
    {
        public int Id {get; set;}
        public string Name {get;set;} = default!;
        public string? Phone {get;set;}
        public string? Location {get;set;}
        public bool Active { get; set; } = true;
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime DateEdit { get; set; }
    }
}