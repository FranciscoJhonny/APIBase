namespace DevFM.WebApi.Dtos
{
    public class PacoteDto
    {
        public int PacoteId { get; set; }
        public string Descricao_Pacote { get; set; }
        public decimal Valor_Pacote { get; set; }
        public IEnumerable<Paciente_PacoteDto> Paciente_PacoteDtos { get; set; }
    }
}
