namespace DevFM.WebApi.Dtos
{
    public class PacienteDto
    {
        public int PacienteId { get; set; }
        public int MunicipioId { get; set; }
        public string NomePaciente { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataRenovacao { get; set; }
        public string DecricaoPaciente { get; set; } = string.Empty;
        public string Observaçao { get; set; } = string.Empty;
        public string Particulariedade { get; set; } = string.Empty;
        public string Jornada { get; set; } = string.Empty;
        public IEnumerable<TelefoneDto> TelefonesPacienteDtos { get; set; }
        public IEnumerable<EnderecoDto> EnderecosPacienteDtos { get; set; } 
        public IEnumerable<ResponsavelDto> ResponsaveisPacienteDtos { get; set; }
        public IEnumerable<AtendimentoDto> AtendimentosPacienteDtos { get; set; }
        public IEnumerable<Paciente_PacoteDto> Paciente_PacoteDtos { get; set; }
    }
}

