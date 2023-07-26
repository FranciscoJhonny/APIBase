namespace DevFM.WebApi.Dtos
{
    public class PacoteDto
    {
        public int PacoteId { get; set; }
        public string DescricaoPacote { get; set; }
        public decimal ValorPacote { get; set; }
        public IEnumerable<Paciente_PacoteDto> Paciente_PacoteDtos { get; set; }
    }
}
