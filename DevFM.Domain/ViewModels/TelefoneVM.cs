using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Domain.ViewModels
{
    /// <summary>
    /// VM Telefone
    /// </summary>
    public class TelefoneVM
    {

        /// <summary>
        /// Telefone no sistema
        /// </summary>
        public int TelefoneId { get; set; }

        public string NumeroTelefone { get; set; }

        public int CuidadorId { get; set; }
        public string DescricaoTipoTelefone { get; set; } 

    }
}
