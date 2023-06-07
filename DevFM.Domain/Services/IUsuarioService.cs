using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ObterUsuarioAsync();
        Task<Usuario> ObterUsuarioPorIdAsync(int usuarioId);
        Task<int> NewUsuarioAsync(Usuario usuario);
    }
}
