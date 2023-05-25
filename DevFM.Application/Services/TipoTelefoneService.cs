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
    public class TipoTelefoneService : ITipoTelefoneService
    {
        private readonly ITipoTelefoneSqlReadAdapter _tipoTelefoneSqlAdapter;

        public TipoTelefoneService(ITipoTelefoneSqlReadAdapter tipoTelefoneSqlAdapter)
        {
            _tipoTelefoneSqlAdapter = tipoTelefoneSqlAdapter;
        }

        public async Task<IEnumerable<TipoTelefone>> ObterTipoTelefoneAsync()
        {
            return await _tipoTelefoneSqlAdapter.ObterTipoTelefoneAsync();
        }

        public async Task<TipoTelefone> ObterTipoTelefonePorIdAsync(int tipoTelefoneId)
        {
            return await _tipoTelefoneSqlAdapter.ObterTipoTelefonePorIdAsync(tipoTelefoneId);
        }
    }
}
