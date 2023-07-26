using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using DevFM.Domain.ViewModels;
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
                                        ,[CategoriaId]
                                        ,[DataAlteracao]
                                        ,[Ativo]
                                   FROM [dbo].[Cuidadores]";

            return await _connection.QueryAsync<Cuidador>(sql, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<Telefone>> ObterTelefonesCuidadorAsync(int cuidadorId)
        {
            const string sql = @"SELECT t.TelefoneId,t.NumeroTelefone, t.TipoTelefoneId,tt.DescricaoTipoTelefone FROM dbo.Cuidadores c
                                        JOIN dbo.Cuidador_Telefones ct ON ct.CuidadorId = c.CuidadorId
                                        JOIN dbo.Telefones t ON t.TelefoneId = ct.TelefoneId
                                        JOIN dbo.TipoTelefones tt ON tt.TipoTelefoneId = t.TipoTelefoneId
                                        WHERE c.CuidadorId =  @cuidadorId";

            return await _connection.QueryAsync<Telefone>(sql, new { cuidadorId }, commandType: CommandType.Text);
        }
        public async Task<Cuidador> ObterCuidadorPorIdAsync(int cuidadorId)
        {
            const string sql = @"SELECT  [CuidadorId]
                                        ,[NomeCuidador]
                                        ,[CategoriaId]
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

        public async Task<int> UpdateCuidador(Cuidador cuidador)
        {
            const string sql = @"UPDATE [dbo].[Cuidadores]
                                   SET [NomeCuidador] = @NomeCuidador
                                      ,[CategoriaId] = @CategoriaId
                                      ,[DataAlteracao] = GETDATE()
                                      ,[Ativo] = @Ativo
                                 WHERE [CuidadorId] = @CuidadorId";

            await _connection.ExecuteScalarAsync<int>(sql, cuidador, commandType: CommandType.Text);

            const string sql_telefoneId = @"SELECT TelefoneId FROM dbo.Cuidador_Telefones WHERE CuidadorId = @CuidadorId";


            var listTelefoneId = await _connection.QueryAsync<int>(sql_telefoneId, new { cuidador.CuidadorId }, commandType: CommandType.Text);

            const string sql_delete_cuidador_telefone = @"DELETE FROM dbo.Cuidador_Telefones WHERE CuidadorId = @CuidadorId";

            var id = await _connection.ExecuteScalarAsync<int>(sql_delete_cuidador_telefone, new { cuidador.CuidadorId }, commandType: CommandType.Text);

            foreach (var telefoneId in listTelefoneId)
            {
                const string sql_delete_telefone = @"DELETE FROM dbo.Telefones WHERE TelefoneId = @TelefoneId";
                 await _connection.ExecuteScalarAsync<int>(sql_delete_telefone, new { telefoneId }, commandType: CommandType.Text);
            }

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

                await _connection.ExecuteScalarAsync<int>(sql_cuidador_telefone, new { cuidador.CuidadorId, telefoneId }, commandType: CommandType.Text);

            }
            return id;
        }


    }
}
