namespace DevFM.WebApi.Dtos
{
    public class TipoTelefoneDto
    {
        public int TipoTelefoneId { get; set; }
        public string DescricaoTipoTelefone { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
