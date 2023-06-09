﻿namespace DevFM.WebApi.Dtos
{
    public class PacientePostDto
    {
        public int MunicipioId { get; set; }
        public string NomePaciente { get; set; } 
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataRenovacao { get; set; }
        public string DecricaoPaciente { get; set; }
        public string Observaçao { get; set; } 
        public string Particulariedade { get; set; }
        public string Jornada { get; set; } 
        public IEnumerable<TelefonePostDto> TelefonesPacientePostDtos { get; set; } 
        public IEnumerable<EnderecoPostDto> EnderecosPacientePostDtos { get; set; } 
        public IEnumerable<ResponsavelPostDto> ResponsaveisPacientePostDtos { get; set; } 
        public IEnumerable<AtendimentoPostDto> AtendimentosPacientePostDtos { get; set; }
        public IEnumerable<Paciente_PacotePostDto> Paciente_PacotePostPostDtos { get; set; }
    }
}

