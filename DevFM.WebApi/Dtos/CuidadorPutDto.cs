namespace DevFM.WebApi.Dtos
{
    public class CuidadorPutDto
    {
        public int CuidadorId { get; set; }
        public string NomeCuidador { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<TelefonePutDto> TelefonesCuidador { get; set; }
    }
}
