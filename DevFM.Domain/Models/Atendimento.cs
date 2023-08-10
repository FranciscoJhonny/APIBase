using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Atendimento
    {
        public int AtendimentoId { get; set; }
        public int PacienteId { get; set; }
        public int CuidadorId { get; set; }
        public string NomeCuidador { get; set; }
        public int TurnoId { get; set; }
        public string DescricaoTurno { get; set; }
        public string ProfissionalCor { get; set; }         
        public string DescricaoCategoria { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public Paciente Paciente { get; set; } 
        public Cuidador Cuidador { get; set; } 
        public Turno Turno { get; set; }
        public IEnumerable<Telefone> TelefonesCuidador { get; set; } = Enumerable.Empty<Telefone>();

    }
}
