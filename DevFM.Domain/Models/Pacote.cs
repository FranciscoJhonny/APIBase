using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Pacote
    {
        public int PacoteId { get; set; }
        public string Descricao_Pacote { get; set; } 
        public decimal Valor_Pacote { get;set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Paciente_Pacote> Paciente_Pacotes { get; set; } = Enumerable.Empty<Paciente_Pacote>();
        
    }
}
