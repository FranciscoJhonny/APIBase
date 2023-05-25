using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface IEstadoCivilService
    {
        Task<IEnumerable<EstadoCivil>> ObterEstadoCivilAsync();
        Task<EstadoCivil> ObterEstadoCivilPorIdAsync(int estadoCivilId);
    }
}
