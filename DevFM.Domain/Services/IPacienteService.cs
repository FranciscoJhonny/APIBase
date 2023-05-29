using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface IPacienteService
    {
        Task<IEnumerable<Paciente>> ObterPacienteAsync();
        Task<Paciente> ObterPacientePorIdAsync(int pacienteId);
        Task<int> NewPacienteAsync(Paciente paciente);
    }
}
