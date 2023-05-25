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
    public class EstadoCivilService : IEstadoCivilService
    {
        private readonly IEstadoCivilSqlReadAdapter _EstadoCivilSqlAdapter;

        public EstadoCivilService(IEstadoCivilSqlReadAdapter EstadoCivilSqlAdapter)
        {
            _EstadoCivilSqlAdapter = EstadoCivilSqlAdapter;
        }

        public async Task<IEnumerable<EstadoCivil>> ObterEstadoCivilAsync()
        {
            return await _EstadoCivilSqlAdapter.ObterEstadoCivilAsync();
        }

        public async Task<EstadoCivil> ObterEstadoCivilPorIdAsync(int estadoCivilId)
        {
            return await _EstadoCivilSqlAdapter.ObterEstadoCivilPorIdAsync(estadoCivilId);
        }
    }
}
