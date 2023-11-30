using DevFM.Domain.Models;

namespace DevFM.WebApi.Dtos
{
    public class UsuarioPutDto
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int PerfilId { get; set; }
    }
}
