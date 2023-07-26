using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.ViewModels
{
    /// <summary>
    /// DTO drop muncipio
    /// </summary>
    public class UsuarioLogadoVM
    {

        /// <summary>
        /// Usuario logado no sistema
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Nome do Usuario Logado no sistema
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Perfil do usuário
        /// </summary>
        public int PerfilId { get; set; }
        public string Email { get; set; } 

    }
}
