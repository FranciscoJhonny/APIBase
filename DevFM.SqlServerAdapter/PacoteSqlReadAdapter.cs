using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class PacoteSqlReadAdapter : IPacoteSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static PacoteSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public PacoteSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<PacoteSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
        public async Task<IEnumerable<Pacote>> ObterPacoteAsync()
        {
            const string sql = @"SELECT [PacoteId]
                                        ,[Descricao_Pacote] as [DescricaoPacote]
                                        ,[Valor_Pacote] as [ValorPacote]
                                        ,[DataCriacao]
                                        ,[DataAlteracao]
                                        ,[Ativo]
                                    FROM [dbo].[Pacotes]
                                 ORDER BY DescricaoPacote";

            return await _connection.QueryAsync<Pacote>(sql, commandType: CommandType.Text);
        }
        public async Task<Pacote> ObterPacotePorIdAsync(int pacoteId)
        {
            const string sql = @"SELECT [PacoteId]
                                       ,[Descricao_Pacote] as [DescricaoPacote]
                                        ,[Valor_Pacote] as [ValorPacote]
                                        ,[DataCriacao]
                                        ,[DataAlteracao]
                                        ,[Ativo]
                                    FROM [dbo].[Pacotes]
                                WHERE [PacoteId] = @PacoteId";

            return await _connection.QueryFirstOrDefaultAsync<Pacote>(sql, new { pacoteId }, commandType: CommandType.Text);
        }

        public async Task<int> NewPacoteAsync(Pacote pacote)
        {
            const string sql = @"INSERT INTO [dbo].[Pacotes]
                                                   ([Descricao_Pacote]
                                                   ,[Valor_Pacote]
                                                   ,[DataCriacao]
                                                   ,[DataAlteracao]
                                                   ,[Ativo])
	                                        	   OUTPUT INSERTED.PacoteId
                                             VALUES
                                                   (@Descricao_Pacote
                                                   ,@Valor_Pacote
                                                   ,GETDATE() 
                                                   ,GETDATE()
                                                   ,1)";

            return await _connection.ExecuteScalarAsync<int>(sql, pacote, commandType: CommandType.Text);
        }

        public async Task<int> UpdatePacote(Pacote pacote)
        {
            const string sql = @"UPDATE [dbo].[Pacotes]
                                          SET [Descricao_Pacote] = @Descricao_Pacote
                                             ,[Valor_Pacote] = @Valor_Pacote
                                             ,[DataAlteracao] = GETDATE()
                                             ,[Ativo] = @Ativo
                                  WHERE [PacoteId] = @PacoteId";
            return await _connection.ExecuteScalarAsync<int>(sql, pacote, commandType: CommandType.Text);
        }
    }
}