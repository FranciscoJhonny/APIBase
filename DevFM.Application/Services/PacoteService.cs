using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using DevFM.Domain.Services;

namespace DevFM.Application.Services
{
    public class PacoteService : IPacoteService
    {
        private readonly IPacoteSqlReadAdapter _pacoteSqlAdapter;

        public PacoteService(IPacoteSqlReadAdapter pacoteSqlAdapter)
        {
            _pacoteSqlAdapter = pacoteSqlAdapter;
        }

        public async Task<IEnumerable<Pacote>> ObterPacoteAsync()
        {
            return await _pacoteSqlAdapter.ObterPacoteAsync();
        }

        public async Task<Pacote> ObterPacotePorIdAsync(int pacoteId)
        {
            return await _pacoteSqlAdapter.ObterPacotePorIdAsync(pacoteId);
        }

        public async Task<int> NewPacoteAsync(Pacote pacote)
        {
            return await _pacoteSqlAdapter.NewPacoteAsync(pacote);
        }

        public async Task<int> UpdatePacote(Pacote pacote)
        {
            return await _pacoteSqlAdapter.UpdatePacote(pacote);
        }
    }
}
