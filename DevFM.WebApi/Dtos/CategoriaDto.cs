namespace DevFM.WebApi.Dtos
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }
        public string DescricaoCategoria { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
