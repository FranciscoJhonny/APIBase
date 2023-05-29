using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class CuidadorSqlReadAdapter : ICuidadorSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static CuidadorSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public CuidadorSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<CuidadorSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
        public async Task<IEnumerable<Cuidador>> ObterCuidadorAsync()
        {
            const string sql = @"SELECT  [CuidadorId]
                                        ,[NomeCuidador]
                                        ,[DataCriacao]
                                        ,[DataAlteracao]
                                        ,[Ativo]
                                   FROM [dbo].[Cuidadores]";

            return await _connection.QueryAsync<Cuidador>(sql, commandType: CommandType.Text);
        }
        public async Task<Cuidador> ObterCuidadorPorIdAsync(int cuidadorId)
        {
            const string sql = @"SELECT  [CuidadorId]
                                        ,[NomeCuidador]
                                        ,[DataCriacao]
                                        ,[DataAlteracao]
                                        ,[Ativo]
                                   FROM [dbo].[Cuidadores]
                                WHERE [CuidadorId] = @cuidadorId";

            return await _connection.QueryFirstOrDefaultAsync<Cuidador>(sql, new { cuidadorId }, commandType: CommandType.Text);
        }

        public async Task<int> NewCuidadorAsync(Cuidador cuidador)
        {
            const string sql = @"INSERT dbo.Cuidadores
                                        ( NomeCuidador 
                                         ,CategoriaId 
                                         ,DataAlteracao
                                         ,Ativo)
                               OUTPUT INSERTED.CuidadorId
                                VALUES  ( @NomeCuidador 
                                         ,@CategoriaId
                                         ,GETDATE()
                                         ,1)";

            var cuidadorId = await _connection.ExecuteScalarAsync<int>(sql, cuidador, commandType: CommandType.Text);

            foreach (var item in cuidador.TelefonesCuidador)
            {
                const string sql_telefone = @"INSERT dbo.Telefones
                                                       ( NumeroTelefone ,
                                                         TipoTelefoneId ,
                                                         DataCriacao ,
                                                         DataAlteracao)
                                                OUTPUT INSERTED.TelefoneId
                                               VALUES  ( @NumeroTelefone ,
                                                         @TipoTelefoneId ,
                                                         GETDATE() , 
                                                         GETDATE())";

                int telefoneId = await _connection.ExecuteScalarAsync<int>(sql_telefone, item, commandType: CommandType.Text);


                const string sql_cuidador_telefone = @"INSERT dbo.Cuidador_Telefones
                                                                    ( CuidadorId ,
                                                                      TelefoneId ,
                                                                      DataCriacao ,
                                                                      DataAlteracao
                                                                    )
                                                            VALUES  ( @CuidadorId ,
                                                                      @TelefoneId ,
                                                                      GETDATE() ,
                                                                      GETDATE())";

                await _connection.ExecuteScalarAsync<int>(sql_cuidador_telefone, new { cuidadorId, telefoneId }, commandType: CommandType.Text);

            }
            return cuidadorId;
        }
    }
}
