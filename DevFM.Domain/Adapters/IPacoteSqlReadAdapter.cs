using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface IPacoteSqlReadAdapter
    {
        Task<IEnumerable<Pacote>> ObterPacoteAsync();
        Task<Pacote> ObterPacotePorIdAsync(int pacoteId);
        Task<int> NewPacoteAsync(Pacote pacote);
        Task<int> UpdatePacote(Pacote pacote);

    }
}
