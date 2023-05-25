using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface IMunicipioService
    {
        Task<IEnumerable<Municipio>> ObterMunicipioAsync();
        Task<Municipio> ObterMunicipioPorIdAsync(int municipioId);
        Task<IEnumerable<Municipio>> ObterMunicipioPorEstadoIdAsync(int estadoId);
    }
}
