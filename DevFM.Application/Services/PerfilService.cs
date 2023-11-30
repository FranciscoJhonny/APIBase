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
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilSqlReadAdapter _PerfilSqlAdapter;

        public PerfilService(IPerfilSqlReadAdapter PerfilSqlAdapter)
        {
            _PerfilSqlAdapter = PerfilSqlAdapter;
        }

        public async Task<IEnumerable<Perfil>> ObterPerfilAsync()
        {
            return await _PerfilSqlAdapter.ObterPerfilAsync();
        }

        public async Task<Perfil> ObterPerfilPorIdAsync(int perfilId)
        {
            return await _PerfilSqlAdapter.ObterPerfilPorIdAsync(perfilId);
        }
    }
}
