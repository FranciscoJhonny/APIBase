using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public string Logradouro { get; set; } 
        public string Numero { get; set; }
        public string Complemento { get; set; } 
        public string Cep { get; set; } 
        public string Bairro { get; set; } 
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
    }
}
