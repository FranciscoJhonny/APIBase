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
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoSqlReadAdapter _TurnoSqlAdapter;

        public TurnoService(ITurnoSqlReadAdapter TurnoSqlAdapter)
        {
            _TurnoSqlAdapter = TurnoSqlAdapter;
        }

        public async Task<IEnumerable<Turno>> ObterTurnoAsync()
        {
            return await _TurnoSqlAdapter.ObterTurnoAsync();
        }

        public async Task<Turno> ObterTurnoPorIdAsync(int turnoId)
        {
            return await _TurnoSqlAdapter.ObterTurnoPorIdAsync(turnoId);
        }
    }
}
