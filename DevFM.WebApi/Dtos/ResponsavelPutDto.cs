namespace DevFM.WebApi.Dtos
{
    public class ResponsavelPutDto
    {
        public int ResponsavelId { get; set; }
        public string NomeResponsavel { get; set; }
        public IEnumerable<TelefonePutDto> TelefonesResponsavel { get; set; }
    }
}
