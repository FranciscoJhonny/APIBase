namespace DevFM.WebApi.Dtos
{
    public class AtendimentoPutDto
    {
        public int AtendimentoId { get; set; }
        public int CuidadorId { get; set; }
        public int TurnoId { get; set; }
        public string ProfissionalCor { get; set; }
        public DateTime DataInicio { get; set; }
    }
}
