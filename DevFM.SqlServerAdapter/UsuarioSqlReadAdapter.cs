using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class UsuarioSqlReadAdapter : IUsuarioSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static UsuarioSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public UsuarioSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<UsuarioSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
        public async Task<IEnumerable<Usuario>> ObterUsuarioAsync()
        {
            const string sql = @"SELECT UsuarioId ,
                                        Nome ,
                                        Email ,
                                        PerfilId ,
                                        Ativo ,
                                        DataInclusao ,
                                        DataOperacao ,
                                        Senha ,
                                        TokenRecuperacaoSenha ,
                                        DataRecuperacaoSenha FROM dbo.Usuario";

            return await _connection.QueryAsync<Usuario>(sql, commandType: CommandType.Text);
        }
        public async Task<Usuario> ObterUsuarioPorIdAsync(int usuarioId)
        {
            const string sql = @"SELECT UsuarioId ,
                                        Nome ,
                                        Email ,
                                        PerfilId ,
                                        Ativo ,
                                        DataInclusao ,
                                        DataOperacao ,
                                        Senha ,
                                        TokenRecuperacaoSenha ,
                                        DataRecuperacaoSenha FROM dbo.Usuario
                                WHERE [UsuarioId] = @usuarioId";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { usuarioId }, commandType: CommandType.Text);
        }

        public async Task<int> NewUsuarioAsync(Usuario Usuario)
        {
            const string sql = @"INSERT INTO [dbo].[Usuario]
                                                   ([Nome]
                                                   ,[Email]
                                                   ,[PerfilId]
                                                   ,[Ativo]
                                                   ,[DataInclusao]
                                                   ,[DataOperacao]
                                                   ,[Senha]
                                                   ,[TokenRecuperacaoSenha]
                                                   ,[DataRecuperacaoSenha])
	                                        	   OUTPUT INSERTED.UsuarioId
                                             VALUES
                                                   (@Nome
                                                   ,@Email
                                                   ,@PerfilId
                                                   ,1
                                                   ,GETDATE() 
                                                   ,GETDATE()
                                                   ,@Senha
                                                   ,NULL
                                                   ,NULL)";

            return await _connection.ExecuteScalarAsync<int>(sql, Usuario, commandType: CommandType.Text); ;
        }
    }
}
