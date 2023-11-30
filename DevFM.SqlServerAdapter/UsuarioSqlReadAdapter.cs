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
            const string sql = @"SELECT * FROM dbo.Usuario u JOIN dbo.Perfil p ON p.PerfilId = u.PerfilId;";

            var retorno = await _connection.QueryAsync<Usuario, Perfil, Usuario>(sql,
                (usuario, perfil) =>
                {
                    if (perfil is not null) usuario.Perfil = perfil;
                    return usuario;
                }, splitOn: "PerfilId", commandType: CommandType.Text);
          
            return retorno;
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
                                        DataRecuperacaoSenha FROM Usuario
                                WHERE UsuarioId = @usuarioId";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { usuarioId }, commandType: CommandType.Text);
        }

        public async Task<int> NewUsuarioAsync(Usuario Usuario)
        {
            const string sql = @"INSERT INTO Usuario
                                                   (Nome
                                                   ,Email
                                                   ,PerfilId
                                                   ,Ativo
                                                   ,DataInclusao
                                                   ,DataOperacao
                                                   ,Senha
                                                   ,TokenRecuperacaoSenha
                                                   ,DataRecuperacaoSenha)
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

        public async Task<Usuario> ObterPorUsuarioSenhaAsync(string login, string senha)
        {
            const string sql = @"SELECT UsuarioId ,
                                        Nome ,
                                        Email ,
                                        PerfilId ,
                                        Ativo ,                                        
                                        Senha ,
                                        TokenRecuperacaoSenha ,
                                        DataRecuperacaoSenha FROM Usuario
	                                    WHERE Email = @login
	                                    AND Senha = @senha";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { login, senha }, commandType: CommandType.Text);
        }
        public async Task<int> VerificaUsuarioAsync(string email)
        {
            const string sql = @"SELECT COUNT(UsuarioId)FROM Usuario WHERE Email = @Email;";

            return await _connection.ExecuteScalarAsync<int>(sql, new { email }, commandType: CommandType.Text);
        }

        public async Task<Perfil> ObterPerfioPorIdAsync(int perfioId)
        {
            const string sql = @"SELECT PerfilId ,
                                        Descricao ,
                                        Ativo ,
                                        DataInclusao ,
                                        DataOperacao FROM Perfil WHERE PerfilId = @perfioId";

            return await _connection.QueryFirstOrDefaultAsync<Perfil>(sql, new { perfioId }, commandType: CommandType.Text);
        }

        public async Task<int> UpdateUsuario(Usuario Usuario)
        {
            const string sql = @"UPDATE [dbo].[Usuario]
                                      SET [Nome] = @Nome
                                         ,[Email] = @Email
                                         ,[PerfilId] = @PerfilId
                                         ,[Ativo] = @Ativo      
                                         ,[DataOperacao] = GETDATE()
                                         ,[Senha] = @Senha
                                    WHERE UsuarioId = @UsuarioId";

            return await _connection.ExecuteScalarAsync<int>(sql, Usuario, commandType: CommandType.Text); ;
        }
        public async Task<bool> DeleteUsuarioPorIdAsync(int usuarioId)
        {
            const string sql = @"UPDATE [dbo].[Usuario]
                                      SET [Ativo] = false 
                                    WHERE UsuarioId = @UsuarioId";
            
            return await _connection.ExecuteScalarAsync<bool>(sql, usuarioId, commandType: CommandType.Text); ;
        }
    }
}
