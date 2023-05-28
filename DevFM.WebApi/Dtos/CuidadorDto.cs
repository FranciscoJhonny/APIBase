namespace DevFM.WebApi.Dtos
{
    public class CuidadorDto
    {
        public int CuidadorId { get; set; }
        public string NomeCuidador { get; set; } = string.Empty;
        public IEnumerable<TelefoneDto> TelefonesCuidador { get; set; }
    }
}
