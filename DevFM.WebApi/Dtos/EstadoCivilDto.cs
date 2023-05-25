namespace DevFM.WebApi.Dtos
{
    public class EstadoCivilDto
    {
        public int EstadoCivilId { get; set; }
        public string DescricaoEstadoCivil { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
