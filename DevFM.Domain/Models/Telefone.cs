using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Telefone
    {
        public int TelefoneId { get; set; } 
        public int TipoTelefoneId { get; set; }
        public string NumeroTelefone { get; set; } = string.Empty;
        public string DescricaoTipoTelefone { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Cuidador Cuidador { get; set; } = new Cuidador();
        public Paciente Paciente { get; set; } = new Paciente();
    }
}
