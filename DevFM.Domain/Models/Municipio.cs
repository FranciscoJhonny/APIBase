namespace DevFM.Domain.Models
{
    public class Municipio
    {
        public int MunicipioId { get; set; }
        public int EstadoId { get; set; }
        public string DescricaoMunicipio { get; set; } = string.Empty;  
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
