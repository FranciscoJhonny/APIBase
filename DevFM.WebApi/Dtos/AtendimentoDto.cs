namespace DevFM.WebApi.Dtos
{
    public class AtendimentoDto
    {

        public int AtendimentoId { get; set; }
        public int PacienteId { get; set; }
        public int CuidadorId { get; set; }
        public string NomeCuidador { get; set; }
        public int TurnoId { get; set; }
        public string Turno { get; set; }
        public string ProfissionalCor { get; set; }
        public string DescricaoCategoria { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<TelefoneDto> TelefonesCuidador { get; set; } = Enumerable.Empty<TelefoneDto>();
    }
}
