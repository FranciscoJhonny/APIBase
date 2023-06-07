using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int PerfilId { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string TokenRecuperacaoSenha { get; set; } = string.Empty;
        public DateTime? DataRecuperacaoSenha { get; set; }
        public bool Ativo { get; set; }
        public Perfil Perfil{ get; set; } = new Perfil();
    }
}
