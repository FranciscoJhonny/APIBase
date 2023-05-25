using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface ITipoTelefoneService
    {
        Task<IEnumerable<TipoTelefone>> ObterTipoTelefoneAsync();
        Task<TipoTelefone> ObterTipoTelefonePorIdAsync(int tipoTelefoneId);
    }
}
