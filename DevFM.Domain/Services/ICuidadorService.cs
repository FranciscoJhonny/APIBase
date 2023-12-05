using DevFM.Domain.Models;
using DevFM.Domain.ViewModels;

namespace DevFM.Domain.Services
{
    public interface ICuidadorService
    {
        Task<IEnumerable<Cuidador>> ObterCuidadorAsync();
        Task<IEnumerable<Cuidador>> ObterCuidadorNomeAsync(int filtro, string nome);
        Task<Cuidador> ObterCuidadorPorIdAsync(int cuidadorId);
        Task<int> NewCuidadorAsync(Cuidador cuidador);
        Task<IEnumerable<Telefone>> ObterTelefonesCuidadorAsync(int cuidadorId);
        Task<int> UpdateCuidador(Cuidador cuidador);
        Task<bool> DeleteCuidadorPorIdAsync(int cuidadorId);
        Task<bool> VerificaCuidadorAsync(string nomeCuidador);

    }
}
