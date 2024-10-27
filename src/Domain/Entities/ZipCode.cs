namespace Domain.Entities
{
    public class ZipCode
    {
        public string cep { get; set; } = default!;
        public string? logradouro { get; set; }
        public string? complemento { get; set; }
        public string? unidade { get; set; }
        public string? bairro { get; set; }
        public string localidade { get; set; } = default!;
        public string uf { get; set; } = default!;
        public string estado { get; set; } = default!;
        public string? regiao { get; set; }
        public string? ibge { get; set; }
        public string? gia { get; set; }
        public string? ddd { get; set; }
        public string? siafi { get; set;}
    }
}
