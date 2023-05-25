using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface IEstadoSqlReadAdapter
    {
        Task<IEnumerable<Estado>> ObterEstadoAsync();
        Task<Estado> ObterEstadoPorIdAsync(int estadoId);

    }
}
