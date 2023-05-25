namespace DevFM.WebApi.Dtos
{
    public class EstadoDto
    {
        public int EstadoId { get; set; }
        public string DescricaoEstado { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
