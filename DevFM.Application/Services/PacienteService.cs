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
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteSqlReadAdapter _pacienteSqlAdapter;

        public PacienteService(IPacienteSqlReadAdapter pacienteSqlAdapter)
        {
            _pacienteSqlAdapter = pacienteSqlAdapter;
        }

        public async Task<IEnumerable<Paciente>> ObterPacienteAsync()
        {
            return await _pacienteSqlAdapter.ObterPacienteAsync();
        }

        public async Task<Paciente> ObterPacientePorIdAsync(int pacienteId)
        {
            return await _pacienteSqlAdapter.ObterPacientePorIdAsync(pacienteId);
        }

        public async Task<int> NewPacienteAsync(Paciente paciente)
        {
            return await _pacienteSqlAdapter.NewPacienteAsync(paciente);
        }

    }
}
