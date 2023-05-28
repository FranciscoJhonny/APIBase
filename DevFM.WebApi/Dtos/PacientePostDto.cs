namespace DevFM.WebApi.Dtos
{
    public class PacientePostDto
    {
        public int MunicipioPacienteId { get; set; }
        public int EstadoPacienteId { get; set; }
        public string NomePaciente { get; set; } = string.Empty;
        public DateTime DataNascimentoPaciente { get; set; }
        public string DescricaoPaciente { get; set; } = string.Empty;
        public string ObservacaoPaciente { get; set; } = string.Empty;
        public string ParticularidadePaciente { get; set; } = string.Empty;
        public string JornadaPaciente { get; set; } = string.Empty;
        public IEnumerable<TelefonePostDto> TelefonesPaciente { get; set; }
        public IEnumerable<EnderecoPostDto> EnderecosPaciente { get; set; }
        public IEnumerable<AtendimentoPostDto> AtendimentosPaciente { get; set; }
    }
}

