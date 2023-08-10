namespace DevFM.WebApi.Dtos
{
    public class PacienteDto
    {
        public int PacienteId { get; set; }
        public int MunicipioId { get; set; }
        public string NomePaciente { get; set; } = string.Empty;
        public int Idade { get; set; }  
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataRenovacao { get; set; }
        public string DescricaoPaciente { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
        public string Particulariedade { get; set; } = string.Empty;
        public string Jornada { get; set; } = string.Empty;
        public IEnumerable<TelefoneDto> TelefonesPaciente { get; set; }
        public IEnumerable<EnderecoDto> EnderecosPaciente { get; set; } 
        public IEnumerable<ResponsavelDto> ResponsaveisPaciente { get; set; }
        public IEnumerable<AtendimentoDto> AtendimentosPaciente { get; set; }
        public IEnumerable<Paciente_PacoteDto> Paciente_Pacotes { get; set; }
    }
}

