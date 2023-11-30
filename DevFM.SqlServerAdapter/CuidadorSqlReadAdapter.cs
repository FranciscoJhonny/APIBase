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
            const string sql = @"SELECT * FROM Cuidadores c
                                LEFT JOIN  Cuidador_Telefones ct ON ct.CuidadorId = c.CuidadorId
                                LEFT JOIN  Telefones t ON t.TelefoneId = ct.TelefoneId";
                                    
            var retorno = await _connection.QueryAsync<Cuidador, Telefone, Cuidador>(sql,
                (cuidador, cuidadorTelefone) =>
                {
                    if (cuidadorTelefone is not null) cuidador.Telefones.Add(cuidadorTelefone);
                    return cuidador;
                }, splitOn: "TelefoneId", commandType: CommandType.Text);

            var result = retorno.GroupBy(r => r.CuidadorId).Select(g =>
            {
                var groupedMSAluno = g.First();
                if (groupedMSAluno.Telefones.Count() > 0)
                    groupedMSAluno.Telefones = g.Select(r => r.Telefones.FirstOrDefault()).ToList();
                return groupedMSAluno;
            });

            return result;
        }

        public async Task<IEnumerable<Cuidador>> ObterComMSMatriculaAlunoTurmaAsync()
        {
            const string sql = @"SELECT * FROM Pacientes p
                                    JOIN Paciente_Telefones pt ON pt.PacienteId = p.PacienteId
                                    JOIN Telefones t ON t.TelefoneId = pt.TelefoneId";

            var retorno = await _connection.QueryAsync<Cuidador, Telefone, Cuidador>(sql,
                (msAluno, msMAT) =>
                {
                    msAluno.TelefonesCuidador = new List<Telefone> { msMAT };
                    return msAluno;
                }, splitOn: "TelefoneId", commandType: CommandType.Text);

            return retorno;
        }







        public async Task<IEnumerable<Cuidador>> ObterCuidadorNomeAsync(int filtro, string nome)
        {
            const string sql = @"SELECT  CuidadorId ,
                                         NomeCuidador ,
                                         CategoriaId ,
                                         DataAlteracao ,
                                         Ativo
                                 FROM    Cuidadores
                                 WHERE   1 = 1
                                  AND (@filtro != 1  OR NomeCuidador LIKE ''+ @nome + '%')
                                  AND (@filtro != 2 OR NomeCuidador LIKE '%'+ @nome +'%')";

            return await _connection.QueryAsync<Cuidador>(sql, new {filtro, nome }, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<Telefone>> ObterTelefonesCuidadorAsync(int cuidadorId)
        {
            const string sql = @"SELECT t.TelefoneId,t.NumeroTelefone, t.TipoTelefoneId,tt.DescricaoTipoTelefone FROM Cuidadores c
                                        JOIN Cuidador_Telefones ct ON ct.CuidadorId = c.CuidadorId
                                        JOIN Telefones t ON t.TelefoneId = ct.TelefoneId
                                        JOIN TipoTelefones tt ON tt.TipoTelefoneId = t.TipoTelefoneId
                                        WHERE c.CuidadorId =  @cuidadorId";

            return await _connection.QueryAsync<Telefone>(sql, new { cuidadorId }, commandType: CommandType.Text);
        }
        public async Task<Cuidador> ObterCuidadorPorIdAsync(int cuidadorId)
        {
            const string sql = @"SELECT  CuidadorId
                                        ,NomeCuidador
                                        ,CategoriaId
                                        ,DataAlteracao
                                        ,Ativo
                                   FROM Cuidadores
                                WHERE CuidadorId = @cuidadorId";

            return await _connection.QueryFirstOrDefaultAsync<Cuidador>(sql, new { cuidadorId }, commandType: CommandType.Text);
        }

        public async Task<int> NewCuidadorAsync(Cuidador cuidador)
        {
            const string sql = @"INSERT Cuidadores
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
                const string sql_telefone = @"INSERT Telefones
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


                const string sql_cuidador_telefone = @"INSERT Cuidador_Telefones
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
            const string sql = @"UPDATE Cuidadores
                                   SET NomeCuidador = @NomeCuidador
                                      ,CategoriaId = @CategoriaId
                                      ,DataAlteracao = GETDATE()
                                      ,Ativo = @Ativo
                                 WHERE CuidadorId = @CuidadorId";

            await _connection.ExecuteScalarAsync<int>(sql, cuidador, commandType: CommandType.Text);

            const string sql_telefoneId = @"SELECT TelefoneId FROM Cuidador_Telefones WHERE CuidadorId = @CuidadorId";


            var listTelefoneId = await _connection.QueryAsync<int>(sql_telefoneId, new { cuidador.CuidadorId }, commandType: CommandType.Text);

            const string sql_delete_cuidador_telefone = @"DELETE FROM Cuidador_Telefones WHERE CuidadorId = @CuidadorId";

            var id = await _connection.ExecuteScalarAsync<int>(sql_delete_cuidador_telefone, new { cuidador.CuidadorId }, commandType: CommandType.Text);

            foreach (var telefoneId in listTelefoneId)
            {
                const string sql_delete_telefone = @"DELETE FROM Telefones WHERE TelefoneId = @TelefoneId";
                 await _connection.ExecuteScalarAsync<int>(sql_delete_telefone, new { telefoneId }, commandType: CommandType.Text);
            }

            foreach (var item in cuidador.TelefonesCuidador)
            {
                const string sql_telefone = @"INSERT Telefones
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
               

                const string sql_cuidador_telefone = @"INSERT Cuidador_Telefones
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

        public async Task<bool> DeleteCuidadorPorIdAsync(int cuidadorId)
        {
            

            const string sql_telefoneId = @"SELECT TelefoneId FROM Cuidador_Telefones WHERE CuidadorId = @CuidadorId";


            var listTelefoneId = await _connection.QueryAsync<int>(sql_telefoneId, new { cuidadorId }, commandType: CommandType.Text);

            const string sql_delete_cuidador_telefone = @"DELETE FROM Cuidador_Telefones WHERE CuidadorId = @CuidadorId";

            var id = await _connection.ExecuteScalarAsync<int>(sql_delete_cuidador_telefone, new { cuidadorId }, commandType: CommandType.Text);

            const string sql = @"DELETE FROM Cuidadores                                  
                                 WHERE CuidadorId = @CuidadorId";

            await _connection.ExecuteScalarAsync<int>(sql, new { cuidadorId }, commandType: CommandType.Text);

            foreach (var telefoneId in listTelefoneId)
            {
                const string sql_delete_telefone = @"DELETE FROM Telefones WHERE TelefoneId = @TelefoneId";
                await _connection.ExecuteScalarAsync<int>(sql_delete_telefone, new { telefoneId }, commandType: CommandType.Text);
            }
            return true;
        }
    }
}
