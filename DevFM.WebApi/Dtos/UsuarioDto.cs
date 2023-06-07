using DevFM.Domain.Models;

namespace DevFM.WebApi.Dtos
{
    public class UsuarioDto
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
        public PerfilDto Perfil { get; set; }
    }
}
