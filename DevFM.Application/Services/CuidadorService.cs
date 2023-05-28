using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Application.Services
{
    public class CuidadorService : ICuidadorService
    {
        private readonly ICuidadorSqlReadAdapter _CuidadorSqlAdapter;

        public CuidadorService(ICuidadorSqlReadAdapter CuidadorSqlAdapter)
        {
            _CuidadorSqlAdapter = CuidadorSqlAdapter;
        }

        public async Task<IEnumerable<Cuidador>> ObterCuidadorAsync()
        {
            return await _CuidadorSqlAdapter.ObterCuidadorAsync();
        }

        public async Task<Cuidador> ObterCuidadorPorIdAsync(int CuidadorId)
        {
            return await _CuidadorSqlAdapter.ObterCuidadorPorIdAsync(CuidadorId);
        }

        public async Task<int> NewCuidadorAsync(Cuidador cuidador)
        {
            return await _CuidadorSqlAdapter.NewCuidadorAsync(cuidador);
        }

    }
}
