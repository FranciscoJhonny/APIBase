using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class CategoriaSqlReadAdapter : ICategoriaSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static CategoriaSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public CategoriaSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<CategoriaSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriaAsync()
        {
            const string sql = @"SELECT  CategoriaId
                                        ,DescricaoCategoria
                                        ,DataCriacao
                                        ,DataAlteracao
                                        ,Ativo
                                   FROM Categorias";

            return await _connection.QueryAsync<Categoria>(sql, commandType: CommandType.Text);
        }
        public async Task<Categoria> ObterCategoriaPorIdAsync(int CategoriaId)
        {
            const string sql = @"SELECT  CategoriaId
                                        ,DescricaoCategoria
                                        ,DataCriacao
                                        ,DataAlteracao
                                        ,Ativo
                                   FROM Categorias
                                WHERE CategoriaId = @CategoriaId";

            return await _connection.QueryFirstOrDefaultAsync<Categoria>(sql, new { CategoriaId }, commandType: CommandType.Text);
        }        
    }
}
