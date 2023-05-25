using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface ITipoTelefoneSqlReadAdapter
    {
        Task<IEnumerable<TipoTelefone>> ObterTipoTelefoneAsync();
        Task<TipoTelefone> ObterTipoTelefonePorIdAsync(int tipoTelefoneId);

    }
}
