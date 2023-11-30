using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class TurnoSqlReadAdapter : ITurnoSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static TurnoSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public TurnoSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<TurnoSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<IEnumerable<Turno>> ObterTurnoAsync()
        {
            const string sql = @"SELECT TurnoId
                                       ,DescricaoTurno
                                       ,DataCriacao
                                       ,DataAlteracao
                                   FROM Turnos";

            return await _connection.QueryAsync<Turno>(sql, commandType: CommandType.Text);
        }

        public async Task<Turno> ObterTurnoPorIdAsync(int TurnoId)
        {
            const string sql = @"SELECT TurnoId
                                       ,DescricaoTurno
                                       ,DataCriacao
                                       ,DataAlteracao
                                   FROM Turnos
                                WHERE TurnoId = @TurnoId";

            return await _connection.QueryFirstOrDefaultAsync<Turno>(sql, new { TurnoId }, commandType: CommandType.Text);
        }
    }
}
