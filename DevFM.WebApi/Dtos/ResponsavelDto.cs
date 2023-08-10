namespace DevFM.WebApi.Dtos
{
    public class ResponsavelDto
    {
        public int ResponsavelId { get; set; }
        public string NomeResponsavel { get; set; } = string.Empty;
        public IEnumerable<TelefoneDto> TelefonesResponsavel { get; set; } = Enumerable.Empty<TelefoneDto>();
    }
}
