using DevFM.Domain.Models;
using DevFM.Domain.ViewModels;

namespace DevFM.Domain.Adapters
{
    public interface ICuidadorSqlReadAdapter
    {
        Task<IEnumerable<Cuidador>> ObterCuidadorAsync();
        Task<Cuidador> ObterCuidadorPorIdAsync(int cuidadorId);
        Task<int> NewCuidadorAsync(Cuidador cuidador);
        Task<IEnumerable<Telefone>> ObterTelefonesCuidadorAsync(int cuidadorId);
        Task<int> UpdateCuidador(Cuidador cuidador);

    }
}
