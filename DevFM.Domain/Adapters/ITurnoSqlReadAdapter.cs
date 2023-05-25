using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface ITurnoSqlReadAdapter
    {
        Task<IEnumerable<Turno>> ObterTurnoAsync();
        Task<Turno> ObterTurnoPorIdAsync(int turnoId);

    }
}
