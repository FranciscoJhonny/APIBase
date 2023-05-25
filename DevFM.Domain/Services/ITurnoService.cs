using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface ITurnoService
    {
        Task<IEnumerable<Turno>> ObterTurnoAsync();
        Task<Turno> ObterTurnoPorIdAsync(int turnoId);
    }
}
