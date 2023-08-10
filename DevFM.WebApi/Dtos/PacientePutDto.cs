namespace DevFM.WebApi.Dtos
{
    public class PacientePutDto
    {
        public int PacienteId { get;set; }
        public string NomePaciente { get; set; } 
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataRenovacao { get; set; }
        public string DescricaoPaciente { get; set; }
        public string Observacao { get; set; } 
        public string Particulariedade { get; set; }
        public string Jornada { get; set; }
        public IEnumerable<TelefonePutDto> TelefonesCuidadorPutDtos { get; set; }
        public IEnumerable<EnderecoPutDto> EnderecosPacientePutDtos { get; set; } 
        public IEnumerable<ResponsavelPutDto> ResponsaveisPacientePutDtos { get; set; } 
        public IEnumerable<AtendimentoPutDto> AtendimentosPacientePutDtos { get; set; }
        public IEnumerable<Paciente_PacotePutDto> Paciente_PacotePutDtos { get; set; }
    }
}

