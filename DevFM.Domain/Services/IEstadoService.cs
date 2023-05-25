using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface IEstadoService
    {
        Task<IEnumerable<Estado>> ObterEstadoAsync();
        Task<Estado> ObterEstadoPorIdAsync(int estadoId);
    }
}
