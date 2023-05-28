namespace DevFM.WebApi.Dtos
{
    public class CuidadorPostDto
    {
        
        public string NomeCuidador { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public IEnumerable<TelefonePostDto> TelefonesCuidador { get; set; }
    }
}
