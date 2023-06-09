using DevFM.Domain.Models;

namespace DevFM.WebApi.Dtos
{
    public class LoginUsuarioDto
    {
        /// <summary>
        /// Usuario logado no sistema 
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        /// Nome do Usuario Logado no sistema
        /// </summary>
        public string Senha { get; set; }
    }
}
