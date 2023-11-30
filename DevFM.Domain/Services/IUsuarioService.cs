using DevFM.Domain.Models;
using DevFM.Domain.ViewModels;

namespace DevFM.Domain.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ObterUsuarioAsync();
        Task<Usuario> ObterUsuarioPorIdAsync(int usuarioId);
        Task<int> NewUsuarioAsync(Usuario usuario);
        Task<UsuarioLogadoVM> LoginUsuario(string login, string senha);
        Task<int> VerificaUsuarioAsync(string email);
        Task<int> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuarioPorIdAsync(int usuarioId);
    }
}
