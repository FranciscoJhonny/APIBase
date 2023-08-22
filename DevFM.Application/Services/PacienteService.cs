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
        public async Task<IEnumerable<Paciente>> ObterPacienteParametroAsync(int filtro, string nome)
        {
            return await _pacienteSqlAdapter.ObterPacienteParametroAsync(filtro,nome);
        }

        public async Task<Paciente> ObterPacientePorIdAsync(int pacienteId)
        {
            return await _pacienteSqlAdapter.ObterPacientePorIdAsync(pacienteId);
        }

        public async Task<int> NewPacienteAsync(Paciente paciente)
        {
            return await _pacienteSqlAdapter.NewPacienteAsync(paciente);
        }
        public async Task<IEnumerable<Telefone>> ObterTelefonesPacienteAsync(int pacienteId)
        {
            return await _pacienteSqlAdapter.ObterTelefonesPacienteAsync(pacienteId);
        }
        public async Task<IEnumerable<Endereco>> ObterEnderecoPacienteAsync(int pacienteId)
        {
            return await _pacienteSqlAdapter.ObterEnderecoPacienteAsync(pacienteId);
        }
        public async Task<IEnumerable<Responsavel>> ObterResponsavelPacienteAsync(int pacienteId)
        {
            return await _pacienteSqlAdapter.ObterResponsavelPacienteAsync(pacienteId);
        }

        public async Task<IEnumerable<Telefone>> ObterTelefoneResponsavelAsync(int responsavelId)
        {
            return await _pacienteSqlAdapter.ObterTelefoneResponsavelAsync(responsavelId);
        }
        public async Task<IEnumerable<Atendimento>> ObterAtendimentoPacienteAsync(int pacienteId)
        {
            return await _pacienteSqlAdapter.ObterAtendimentoPacienteAsync(pacienteId);
        }

        public async Task<IEnumerable<Telefone>> ObterTelefoneCuidadorAsync(int cuidadorId)
        {
            return await _pacienteSqlAdapter.ObterTelefoneCuidadorAsync(cuidadorId);
        }

        public async Task<IEnumerable<Paciente_Pacote>> ObterPaciente_PacoteAsync(int pacienteId)
        {
            return await _pacienteSqlAdapter.ObterPaciente_PacoteAsync(pacienteId);
        }

        public async Task<int> UpdatePaciente(Paciente paciente)
        {
            return await _pacienteSqlAdapter.UpdatePaciente(paciente);
        }

    }
}
