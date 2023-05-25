using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class EstadoCivilSqlReadAdapter : IEstadoCivilSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static EstadoCivilSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public EstadoCivilSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<EstadoCivilSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<IEnumerable<EstadoCivil>> ObterEstadoCivilAsync()
        {
            const string sql = @"SELECT [EstadoCivilId]
                                       ,[DescricaoEstadoCivil]
                                       ,[DataCriacao]
                                       ,[DataAlteracao]
                                   FROM [dbo].[EstadoCivis]";

            return await _connection.QueryAsync<EstadoCivil>(sql, commandType: CommandType.Text);
        }

        public async Task<EstadoCivil> ObterEstadoCivilPorIdAsync(int EstadoCivilId)
        {
            const string sql = @"SELECT [EstadoCivilId]
                                       ,[DescricaoEstadoCivil]
                                       ,[DataCriacao]
                                       ,[DataAlteracao]
                                   FROM [dbo].[EstadoCivis]
                                WHERE [EstadoCivilId] = @EstadoCivilId";

            return await _connection.QueryFirstOrDefaultAsync<EstadoCivil>(sql, new { EstadoCivilId }, commandType: CommandType.Text);
        }
    }
}
