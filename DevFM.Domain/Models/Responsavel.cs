using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Responsavel
    {
        public int ResponsavelId { get; set; }
        public int PacienteId { get; set; }
        public string NomeResponsavel { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public Paciente Paciente { get; set; }
        public IEnumerable<Telefone> TelefonesResponsaveis { get; set; } = Enumerable.Empty<Telefone>();
    }
}
