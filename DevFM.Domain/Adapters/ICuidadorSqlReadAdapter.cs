using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface ICuidadorSqlReadAdapter
    {
        Task<IEnumerable<Cuidador>> ObterCuidadorAsync();
        Task<Cuidador> ObterCuidadorPorIdAsync(int cuidadorId);
        Task<int> NewCuidadorAsync(Cuidador cuidador);

    }
}
