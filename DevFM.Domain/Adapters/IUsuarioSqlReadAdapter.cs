using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface IUsuarioSqlReadAdapter
    {
        Task<IEnumerable<Usuario>> ObterUsuarioAsync();
        Task<Usuario> ObterUsuarioPorIdAsync(int UsuarioId);
        Task<int> NewUsuarioAsync(Usuario Usuario);
        Task<Usuario> ObterPorUsuarioSenhaAsync(string login, string senha);
        Task<int> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuarioPorIdAsync(int usuarioId);
        Task<int> VerificaUsuarioAsync(string email);
        Task<Perfil> ObterPerfioPorIdAsync(int PerfioId);
    }
}
