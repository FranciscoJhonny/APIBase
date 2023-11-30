using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class TipoTelefoneSqlReadAdapter : ITipoTelefoneSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static TipoTelefoneSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public TipoTelefoneSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<TipoTelefoneSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<IEnumerable<TipoTelefone>> ObterTipoTelefoneAsync()
        {
            const string sql = @"SELECT TipoTelefoneId
                                       ,DescricaoTipoTelefone
                                       ,DataCriacao
                                       ,DataAlteracao
                                   FROM TipoTelefones";

            return await _connection.QueryAsync<TipoTelefone>(sql, commandType: CommandType.Text);
        }

        public async Task<TipoTelefone> ObterTipoTelefonePorIdAsync(int TipoTelefoneId)
        {
            const string sql = @"SELECT TipoTelefoneId
                                       ,DescricaoTipoTelefone
                                       ,DataCriacao
                                       ,DataAlteracao
                                   FROM TipoTelefones
                                WHERE TipoTelefoneId = @TipoTelefoneId";

            return await _connection.QueryFirstOrDefaultAsync<TipoTelefone>(sql, new { TipoTelefoneId }, commandType: CommandType.Text);
        }
    }
}
