using Dapper;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DevFM.SqlServerAdapter
{

    public class PacienteSqlReadAdapter : IPacienteSqlReadAdapter
    {
        private readonly SqlServerAdapterConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;

        static PacienteSqlReadAdapter() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public PacienteSqlReadAdapter(SqlServerAdapterConfiguration configuration, ILoggerFactory loggerFactory, SqlAdapterContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connection = context.Connection ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory?.CreateLogger<PacienteSqlReadAdapter>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }
        public async Task<IEnumerable<Paciente>> ObterPacienteAsync()
        {
            const string sql = @"SELECT [PacienteId]
                                        ,[MunicipioId]
                                        ,[NomePaciente]
                                        ,[DataNascimento]
                                        ,[DataInicio]
                                        ,[DataRenovacao]
                                        ,[DecricaoPaciente]
                                        ,[Observaçao]
                                        ,[Particulariedade]
                                        ,[Jornada]
                                        ,[DataCriacao]
                                        ,[DataAlteracao]
                                        ,[Ativo]
                                    FROM [dbo].[Pacientes]";

            return await _connection.QueryAsync<Paciente>(sql, commandType: CommandType.Text);
        }
        public async Task<Paciente> ObterPacientePorIdAsync(int pacienteId)
        {
            const string sql = @"SELECT [PacienteId]
                                        ,[MunicipioId]
                                        ,[NomePaciente]
                                        ,[DataNascimento]
                                        ,[DataInicio]
                                        ,[DataRenovacao]
                                        ,[DecricaoPaciente]
                                        ,[Observaçao]
                                        ,[Particulariedade]
                                        ,[Jornada]
                                        ,[DataCriacao]
                                        ,[DataAlteracao]
                                        ,[Ativo]
                                    FROM [dbo].[Pacientes]
                                WHERE [PacienteId] = @PacienteId";

            return await _connection.QueryFirstOrDefaultAsync<Paciente>(sql, new { pacienteId }, commandType: CommandType.Text);
        }

        public async Task<int> NewPacienteAsync(Paciente paciente)
        {
            const string sql = @"INSERT INTO [dbo].[Pacientes]
                                                  ([MunicipioId]
                                                  ,[NomePaciente]
                                                  ,[DataNascimento]
                                                  ,[DataInicio]
                                                  ,[DataRenovacao]
                                                  ,[DecricaoPaciente]
                                                  ,[Observaçao]
                                                  ,[Particulariedade]
                                                  ,[Jornada]
                                                  ,[DataCriacao]
                                                  ,[DataAlteracao]
                                                  ,[Ativo])
	                                        	   OUTPUT INSERTED.PacienteId
                                            VALUES
                                                  (@MunicipioId
                                                  ,@NomePaciente
                                                  ,@DataNascimento
                                                  ,@DataInicio
                                                  ,@DataRenovacao
                                                  ,@DecricaoPaciente
                                                  ,@Observaçao
                                                  ,@Particulariedade
                                                  ,@Jornada
                                                  ,GETDATE()
                                                  ,GETDATE()
                                                  ,1)";

            var pacienteId = await _connection.ExecuteScalarAsync<int>(sql, paciente, commandType: CommandType.Text);

            foreach (var telefone in paciente.TelefonesPacientes)
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

                int telefoneId = await _connection.ExecuteScalarAsync<int>(sql_telefone, telefone, commandType: CommandType.Text);


                const string sql_paciente_telefone = @"INSERT INTO [dbo].[Paciente_Telefones]
                                                                           ([PacienteId]
                                                                           ,[TelefoneId]
                                                                           ,[DataCriacao]
                                                                           ,[DataAlteracao])
                                                                     VALUES
                                                                           (@PacienteId
                                                                           ,@TelefoneId
                                                                           ,GETDATE()
                                                                           ,GETDATE())";

                await _connection.ExecuteScalarAsync<int>(sql_paciente_telefone, new { pacienteId, telefoneId }, commandType: CommandType.Text);

            }

            foreach (var endereco in paciente.EnderecosPaciente)
            {
                const string sql_endereco = @"INSERT INTO [dbo].[Enderecos]
                                                                 ([Logradouro]
                                                                 ,[Numero]
                                                                 ,[Complemento]
                                                                 ,[Cep]
                                                                 ,[Bairro]
                                                                 ,[DataCriacao]
                                                                 ,[DataAlteracao]
                                                                 ,[Ativo])
	                                                       	   OUTPUT INSERTED.EnderecoId
                                                           VALUES
                                                                 (@Logradouro
                                                                 ,@Numero
                                                                 ,@Complemento
                                                                 ,@Cep
                                                                 ,@Bairro
                                                                 ,GETDATE()
                                                                 ,GETDATE()
                                                                 ,1)";

                int enderecoId = await _connection.ExecuteScalarAsync<int>(sql_endereco, endereco, commandType: CommandType.Text);


                const string sql_paciente_endereco = @"INSERT INTO [dbo].[Paciente_Enderecos]
                                                                              ([PacienteId]
                                                                              ,[EnderecoId]
                                                                              ,[DataCriacao]
                                                                              ,[DataAlteracao]
                                                                              ,[Ativo])
                                                                        VALUES
                                                                              (@PacienteId
                                                                              ,@EnderecoId
                                                                              ,GETDATE()
                                                                              ,GETDATE()
                                                                              ,1)";

                await _connection.ExecuteScalarAsync<int>(sql_paciente_endereco, new { pacienteId, enderecoId }, commandType: CommandType.Text);
            }

            foreach (var responsavel in paciente.RespensaveisPacientes)
            {
                var nomeResponsavel = responsavel.NomeResponsavel;
                const string sql_responsavel = @"INSERT INTO [dbo].[Responsaveis]
                                                                  ([PacienteId]
                                                                  ,[NomeResponsavel]
                                                                  ,[DataCriacao]
                                                                  ,[DataAlteracao]
                                                                  ,[Ativo])
                                                            OUTPUT INSERTED.ResponsavelId
                                                            VALUES
                                                                  (@PacienteId 
                                                                  ,@NomeResponsavel
                                                                  ,GETDATE()
                                                                  ,GETDATE()
                                                                  ,1)";

                int responsavelId = await _connection.ExecuteScalarAsync<int>(sql_responsavel, new { pacienteId, nomeResponsavel }, commandType: CommandType.Text);


                foreach (var telefoneresponsavel in responsavel.TelefonesResponsaveis)
                {
                    const string sql_telefone_responsavel = @"INSERT dbo.Telefones
                                                       ( NumeroTelefone ,
                                                         TipoTelefoneId ,
                                                         DataCriacao ,
                                                         DataAlteracao)
                                                OUTPUT INSERTED.TelefoneId
                                               VALUES  ( @NumeroTelefone ,
                                                         @TipoTelefoneId ,
                                                         GETDATE() , 
                                                         GETDATE())";

                    int telefoneId = await _connection.ExecuteScalarAsync<int>(sql_telefone_responsavel, telefoneresponsavel, commandType: CommandType.Text);


                    const string sql_paciente_telefone = @"INSERT INTO [dbo].[Responsavel_Telefones]
                                                                                      ([TelefoneId]
                                                                                      ,[ResponsavelId]
                                                                                      ,[DataCriacao]
                                                                                      ,[DataAlteracao])
                                                                                VALUES
                                                                                      (@TelefoneId
                                                                                      ,@ResponsavelId
                                                                                      ,GETDATE()
                                                                                      ,GETDATE())";

                    await _connection.ExecuteScalarAsync<int>(sql_paciente_telefone, new { responsavelId, telefoneId }, commandType: CommandType.Text);
                }
            }

            foreach (var atendimento in paciente.AtendimentosPacientes)
            {
                atendimento.PacienteId = pacienteId;
                const string sql_atendimento = @"INSERT INTO [dbo].[Atendimentos]
                                                                   ([PacienteId]
                                                                   ,[CuidadorId]
                                                                   ,[TurnoId]
                                                                   ,[ProfissionalCor]
                                                                   ,[DataInicio]
                                                                   ,[DataCriacao]
                                                                   ,[DataAlteracao]
                                                                   ,[Ativo])
                                                             VALUES
                                                                   (@PacienteId
                                                                   ,@CuidadorId
                                                                   ,@TurnoId
                                                                   ,@ProfissionalCor
                                                                   ,@DataInicio
                                                                   ,GETDATE()
                                                                   ,GETDATE()
                                                                   ,1)";

                await _connection.ExecuteScalarAsync<int>(sql_atendimento, atendimento, commandType: CommandType.Text);
            }
            foreach (var paciente_Pacote in paciente.Paciente_Pacotes)
            {
                paciente_Pacote.PacienteId = pacienteId;
                const string sql_paciente_pacote = @"INSERT INTO [dbo].[Paciente_Pacotes]
                                                                       ([PacienteId]
                                                                       ,[PacoteId]
                                                                       ,[PacoteMensal]
                                                                       ,[DiaPlantao]
                                                                       ,[ValorPacote]
                                                                       ,[SalarioCuidador]
                                                                       ,[SalarioDiaCuidador]
                                                                       ,[ValorPlataoCuidador]
                                                                       ,[ValorDesconto]
                                                                       ,[TaxaAdminstrativa]
                                                                       ,[DataCriacao]
                                                                       ,[DataAlteracao]
                                                                       ,[Ativo])
                                                                 VALUES
                                                                       (@PacienteId
                                                                       ,@PacoteId
                                                                       ,@PacoteMensal
                                                                       ,@DiaPlantao
                                                                       ,@ValorPacote
                                                                       ,@SalarioCuidador
                                                                       ,@SalarioDiaCuidador
                                                                       ,@ValorPlataoCuidador
                                                                       ,@ValorDesconto
                                                                       ,@TaxaAdminstrativa
                                                                       ,GETDATE()
                                                                       ,GETDATE()
                                                                       ,1)";

                await _connection.ExecuteScalarAsync<int>(sql_paciente_pacote, paciente_Pacote, commandType: CommandType.Text);
            }

            return pacienteId;
        }
    }
}
