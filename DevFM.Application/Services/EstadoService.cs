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
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoSqlReadAdapter _EstadoSqlAdapter;

        public EstadoService(IEstadoSqlReadAdapter EstadoSqlAdapter)
        {
            _EstadoSqlAdapter = EstadoSqlAdapter;
        }

        public async Task<IEnumerable<Estado>> ObterEstadoAsync()
        {
            return await _EstadoSqlAdapter.ObterEstadoAsync();
        }

        public async Task<Estado> ObterEstadoPorIdAsync(int estadoId)
        {
            return await _EstadoSqlAdapter.ObterEstadoPorIdAsync(estadoId);
        }
    }
}
