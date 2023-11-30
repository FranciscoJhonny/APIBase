using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface IPerfilService
    {
        Task<IEnumerable<Perfil>> ObterPerfilAsync();
        Task<Perfil> ObterPerfilPorIdAsync(int perfilId);
    }
}
