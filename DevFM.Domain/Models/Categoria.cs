using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string DescricaoCategoria { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Cuidador> Cuidadores { get; set; } = Enumerable.Empty<Cuidador>();
    }
}
