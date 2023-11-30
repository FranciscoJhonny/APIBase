using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface IPerfilSqlReadAdapter 
    {
        Task<IEnumerable<Perfil>> ObterPerfilAsync();
        Task<Perfil> ObterPerfilPorIdAsync(int pefilId);

    }
}
