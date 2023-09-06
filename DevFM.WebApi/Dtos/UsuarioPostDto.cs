using DevFM.Domain.Models;

namespace DevFM.WebApi.Dtos
{
    public class UsuarioPostDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int PerfilId { get; set; }
    }
}
