using DevFM.Domain.Models;

namespace DevFM.WebApi.Dtos
{
    public class UsuarioPostDto
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int PerfilId { get; set; }
        public bool Ativo { get; set; }
    }
}
