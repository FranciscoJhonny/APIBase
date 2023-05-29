namespace DevFM.WebApi.Dtos
{
    public class AtendimentoDto
    {
        public int CuidadorId { get; set; }
        public int TurnoId { get; set; }
        public string ProfissionalCor { get; set; }
        public DateTime DataInicio { get; set; }
    }
}
