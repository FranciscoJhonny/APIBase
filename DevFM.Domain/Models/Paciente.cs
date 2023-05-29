using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Paciente
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
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Telefone> TelefonesPacientes {  get; set; } = Enumerable.Empty<Telefone>();
        public IEnumerable<Endereco> EnderecosPaciente { get; set; } = Enumerable.Empty<Endereco>();
        public IEnumerable<Responsavel> RespensaveisPacientes { get; set; } = Enumerable.Empty<Responsavel>();
        public IEnumerable<Atendimento> AtendimentosPacientes { get; set; }= Enumerable.Empty<Atendimento>();
        
    }
}

