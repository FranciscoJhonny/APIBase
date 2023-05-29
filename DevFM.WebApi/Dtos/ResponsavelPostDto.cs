namespace DevFM.WebApi.Dtos
{
    public class ResponsavelPostDto
    {
        public string NomeResponsavel { get; set; } 
        public IEnumerable<TelefonePostDto> TelefonesResponsavel { get; set; }
    }
}
