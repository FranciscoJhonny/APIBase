using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface IPacienteSqlReadAdapter
    {
        Task<IEnumerable<Paciente>> ObterPacienteAsync();
        Task<Paciente> ObterPacientePorIdAsync(int pacienteId);
        Task<int> NewPacienteAsync(Paciente paciente);

    }
}
