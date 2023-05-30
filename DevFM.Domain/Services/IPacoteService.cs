using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface IPacoteService
    {
        Task<IEnumerable<Pacote>> ObterPacoteAsync();
        Task<Pacote> ObterPacotePorIdAsync(int pacoteId);
        Task<int> NewPacoteAsync(Pacote pacote);
        Task<int> UpdatePacote(Pacote pacote);
    }
}
