using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface ICuidadorService
    {
        Task<IEnumerable<Cuidador>> ObterCuidadorAsync();
        Task<Cuidador> ObterCuidadorPorIdAsync(int cuidadorId);
        Task<int> NewCuidadorAsync(Cuidador cuidador);
    }
}
