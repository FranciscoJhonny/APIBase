using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Cuidador
    {
        public int CuidadorId { get; set; }
        public string NomeCuidador { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Telefone> TelefonesCuidador {  get; set; } = Enumerable.Empty<Telefone>();
    }
}
