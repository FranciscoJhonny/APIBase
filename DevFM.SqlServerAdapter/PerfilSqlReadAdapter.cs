using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class PerfilSqlReadAdapter : IPerfilSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static PerfilSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public PerfilSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<PerfilSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<IEnumerable<Perfil>> ObterPerfilAsync()
        {
            const string sql = @"SELECT  PerfilId ,Descricao ,Ativo ,DataInclusao ,DataOperacao FROM Perfil";

            return await _connection.QueryAsync<Perfil>(sql, commandType: CommandType.Text);
        }
        public async Task<Perfil> ObterPerfilPorIdAsync(int PerfilId)
        {
            const string sql = @"SELECT PerfilId ,Descricao ,Ativo ,DataInclusao ,DataOperacao FROM Perfil WHERE PerfilId = @PerfilId";

            return await _connection.QueryFirstOrDefaultAsync<Perfil>(sql, new { PerfilId }, commandType: CommandType.Text);
        }        
    }
}
