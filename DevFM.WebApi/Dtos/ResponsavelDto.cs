namespace DevFM.WebApi.Dtos
{
    public class ResponsavelDto
    {
        public string NomeResponsavel { get; set; } = string.Empty;
        public IEnumerable<TelefoneDto> TelefonesResponsavel { get; set; } = Enumerable.Empty<TelefoneDto>();
    }
}
