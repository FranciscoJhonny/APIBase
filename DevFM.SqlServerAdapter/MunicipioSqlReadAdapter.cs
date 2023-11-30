using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class MunicipioSqlReadAdapter : IMunicipioSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static MunicipioSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public MunicipioSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<MunicipioSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<IEnumerable<Municipio>> ObterMunicipioAsync()
        {
            const string sql = @"SELECT MunicipioId
                                      ,EstadoId
                                      ,DescricaoMunicio
                                      ,DataCriacao
                                      ,DataAlteracao
                                      ,Ativo
                                   FROM Municipios";

            return await _connection.QueryAsync<Municipio>(sql, commandType: CommandType.Text);
        }

        public async Task<Municipio> ObterMunicipioPorIdAsync(int municipioId)
        {
            const string sql = @"SELECT MunicipioId
                                       ,EstadoId
                                      ,DescricaoMunicio
                                      ,DataCriacao
                                      ,DataAlteracao
                                      ,Ativo
                                   FROM Municipios
                                WHERE MunicipioId = @MunicipioId";

            return await _connection.QueryFirstOrDefaultAsync<Municipio>(sql, new { municipioId }, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Municipio>> ObterMunicipioPorEstadoIdAsync(int estadoId)
        {
            const string sql = @"SELECT MunicipioId
                                      ,EstadoId
                                      ,DescricaoMunicio
                                      ,DataCriacao
                                      ,DataAlteracao
                                      ,Ativo
                                   FROM Municipios
                                WHERE EstadoId = @estadoId";

            return await _connection.QueryAsync<Municipio>(sql, new { estadoId }, commandType: CommandType.Text);
        }
    }
}
