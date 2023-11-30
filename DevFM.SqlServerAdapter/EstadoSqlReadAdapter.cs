using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class EstadoSqlReadAdapter : IEstadoSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static EstadoSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public EstadoSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<EstadoSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<IEnumerable<Estado>> ObterEstadoAsync()
        {
            const string sql = @"SELECT EstadoId
                                       ,DescricaoEstado
                                       ,DataCriacao
                                       ,DataAlteracao
                                       ,Ativo
                                       ,Sigla
                                   FROM Estados";

            return await _connection.QueryAsync<Estado>(sql, commandType: CommandType.Text);
        }

        public async Task<Estado> ObterEstadoPorIdAsync(int EstadoId)
        {
            const string sql = @"SELECT EstadoId
                                       ,DescricaoEstado
                                       ,DataCriacao
                                       ,DataAlteracao
                                       ,Ativo
                                       ,Sigla
                                   FROM Estados
                                WHERE EstadoId = @EstadoId";

            return await _connection.QueryFirstOrDefaultAsync<Estado>(sql, new { EstadoId }, commandType: CommandType.Text);
        }
    }
}
