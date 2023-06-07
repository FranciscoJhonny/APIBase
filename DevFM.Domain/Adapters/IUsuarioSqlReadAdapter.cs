using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface IUsuarioSqlReadAdapter
    {
        Task<IEnumerable<Usuario>> ObterUsuarioAsync();
        Task<Usuario> ObterUsuarioPorIdAsync(int UsuarioId);
        Task<int> NewUsuarioAsync(Usuario Usuario);

    }
}
