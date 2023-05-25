using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface IEstadoCivilSqlReadAdapter
    {
        Task<IEnumerable<EstadoCivil>> ObterEstadoCivilAsync();
        Task<EstadoCivil> ObterEstadoCivilPorIdAsync(int estadoCivilId);

    }
}
