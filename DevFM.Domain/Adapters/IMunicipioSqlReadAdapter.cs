using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface IMunicipioSqlReadAdapter
    {
        Task<IEnumerable<Municipio>> ObterMunicipioAsync();
        Task<Municipio> ObterMunicipioPorIdAsync(int municipioId);
        Task<IEnumerable<Municipio>> ObterMunicipioPorEstadoIdAsync(int estadoId);

    }
}
