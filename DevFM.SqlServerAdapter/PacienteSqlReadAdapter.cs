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
            const string sql = @"SELECT * FROM Pacientes p
                                 LEFT JOIN Paciente_Telefones pt ON pt.PacienteId = p.PacienteId
                                 LEFT JOIN Telefones t ON t.TelefoneId = pt.TelefoneId";

            var retorno = await _connection.QueryAsync<Paciente, Telefone, Paciente>(sql,
                (paciente, paicienteTelefone) =>
                {
                    if (paicienteTelefone is not null) paciente.Telefones.Add(paicienteTelefone);
                    return paciente;
                }, splitOn: "TelefoneId", commandType: CommandType.Text);

            var result = retorno.GroupBy(r => r.PacienteId).Select(g =>
            {
                var groupedPaciente = g.First();
                if (groupedPaciente.Telefones.Count() > 0)
                    groupedPaciente.Telefones = g.Select(r => r.Telefones.FirstOrDefault()).ToList();
                return groupedPaciente;
            });

            return result.OrderBy(x=> x.NomePaciente);
        }
        public async Task<IEnumerable<Paciente>> ObterPacienteParametroAsync(int filtro, string nome)
        {
            const string sql = @"SELECT * FROM Pacientes p
                                 LEFT JOIN Paciente_Telefones pt ON pt.PacienteId = p.PacienteId
                                 LEFT JOIN Telefones t ON t.TelefoneId = pt.TelefoneId
                                        WHERE   1 = 1
                                  AND (@filtro != 1  OR p.NomePaciente LIKE ''+ @nome + '%')
                                  AND (@filtro != 2 OR p.NomePaciente LIKE '%'+ @nome +'%')";

            var retorno = await _connection.QueryAsync<Paciente, Telefone, Paciente>(sql,
                 (paciente, paicienteTelefone) =>
                 {
                     if (paicienteTelefone is not null) paciente.Telefones.Add(paicienteTelefone);
                     return paciente;
                 },
                new { filtro, nome }, splitOn: "TelefoneId", commandType: CommandType.Text);

            var result = retorno.GroupBy(r => r.PacienteId).Select(g =>
            {
                var groupedPaciente = g.First();
                if (groupedPaciente.Telefones.Count() > 0)
                    groupedPaciente.Telefones = g.Select(r => r.Telefones.FirstOrDefault()).ToList();
                return groupedPaciente;
            });

            return result;
        }
        public async Task<Paciente> ObterPacientePorIdAsync(int pacienteId)
        {
            const string sql = @"SELECT PacienteId
                                        ,MunicipioId
                                        ,NomePaciente
                                        ,DataNascimento
                                        ,DataInicio
                                        ,DataRenovacao
                                        ,DescricaoPaciente
                                        ,Observacao
                                        ,Particulariedade
                                        ,Jornada
                                        ,DataCriacao
                                        ,DataAlteracao
                                        ,Ativo
                                    FROM Pacientes
                                WHERE PacienteId = @PacienteId";

            return await _connection.QueryFirstOrDefaultAsync<Paciente>(sql, new { pacienteId }, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<Telefone>> ObterTelefonesPacienteAsync(int pacienteId)
        {
            const string sql = @"SELECT t.TelefoneId,t.NumeroTelefone, t.TipoTelefoneId,tt.DescricaoTipoTelefone FROM Paciente_Telefones pa
                                        JOIN Telefones t ON t.TelefoneId = pa.TelefoneId
                                        JOIN TipoTelefones tt ON tt.TipoTelefoneId = t.TipoTelefoneId
                                        WHERE pa.PacienteId = @PacienteId";

            return await _connection.QueryAsync<Telefone>(sql, new { pacienteId }, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<Endereco>> ObterEnderecoPacienteAsync(int pacienteId)
        {
            const string sql = @"SELECT e.EnderecoId,e.Logradouro,e.Numero,e.Complemento,e.Cep,e.Bairro FROM Paciente_Enderecos pe
                                        JOIN Enderecos e ON e.EnderecoId = pe.EnderecoId
                                        WHERE pe.PacienteId = @PacienteId";

            return await _connection.QueryAsync<Endereco>(sql, new { pacienteId }, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<Responsavel>> ObterResponsavelPacienteAsync(int pacienteId)
        {
            const string sql = @"SELECT e.ResponsavelId,e.PacienteId,e.NomeResponsavel FROM Responsaveis e
                                            WHERE e.PacienteId = @PacienteId";

            return await _connection.QueryAsync<Responsavel>(sql, new { pacienteId }, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<Telefone>> ObterTelefoneResponsavelAsync(int responsavelId)
        {
            const string sql = @"SELECT t.TelefoneId,t.NumeroTelefone, t.TipoTelefoneId,tt.DescricaoTipoTelefone FROM Responsavel_Telefones rt
                                    JOIN Telefones t ON t.TelefoneId = rt.TelefoneId
                                    JOIN TipoTelefones tt ON tt.TipoTelefoneId = t.TipoTelefoneId
                                    WHERE rt.ResponsavelId = @ResponsavelId";

            return await _connection.QueryAsync<Telefone>(sql, new { responsavelId }, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<Atendimento>> ObterAtendimentoPacienteAsync(int pacienteId)
        {
            const string sql = @"SELECT  a.AtendimentoId ,
                                          a.PacienteId ,
                                          a.CuidadorId,
                                          c.NomeCuidador ,
                                          t.TurnoId ,
                                          t.DescricaoTurno,
                                          a.ProfissionalCor,
                                  		  a.DataInicio,
										  ca.DescricaoCategoria
                                  FROM    Atendimentos a
                                          JOIN Cuidadores c ON c.CuidadorId = a.CuidadorId
                                          JOIN Turnos t ON t.TurnoId = a.TurnoId
										  JOIN Categorias ca ON ca.CategoriaId = c.CategoriaId
                                  WHERE   a.PacienteId  = @PacienteId";

            return await _connection.QueryAsync<Atendimento>(sql, new { pacienteId }, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<Telefone>> ObterTelefoneCuidadorAsync(int cuidadorId)
        {
            const string sql = @"SELECT  t.TelefoneId ,
                                         t.NumeroTelefone ,
                                         t.TipoTelefoneId ,
                                         tt.DescricaoTipoTelefone
                                 FROM    Cuidador_Telefones ct
                                         JOIN Telefones t ON t.TelefoneId = ct.TelefoneId
                                         JOIN TipoTelefones tt ON tt.TipoTelefoneId = t.TipoTelefoneId
                                 WHERE   ct.CuidadorId = @CuidadorId;";

            return await _connection.QueryAsync<Telefone>(sql, new { cuidadorId }, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<Paciente_Pacote>> ObterPaciente_PacoteAsync(int pacienteId)
        {
            const string sql = @"SELECT pp.Paciente_PacoteId ,
                                          pp.PacienteId ,
                                          pp.PacoteId ,
                                   	      p.Descricao_Pacote AS DescricaoPacoteMensal,
                                          pp.PacoteMensal ,
                                          p.Valor_Pacote ,
                                          pp.ValorPacote ,
                                          pp.DiaPlantao ,
                                          pp.ValorPlantaoPacote ,
                                          pp.SalarioCuidador ,
                                          pp.SalarioDiaCuidador ,
                                          pp.ValorPlantaoCuidador ,
                                          pp.Observacao ,
                                          pp.ValorDesconto ,
                                          pp.ValorAcrescimo ,
                                          pp.TaxaAdminstrativa ,
                                          pp.Ativo FROM Paciente_Pacotes pp
                                   JOIN Pacotes p ON p.PacoteId = pp.PacoteId
                                   WHERE pp.PacienteId = @PacienteId";

            return await _connection.QueryAsync<Paciente_Pacote>(sql, new { pacienteId }, commandType: CommandType.Text);
        }
        public async Task<int> NewPacienteAsync(Paciente paciente)
        {
            const string sql = @"INSERT INTO Pacientes
                                                   (NomePaciente
                                                  ,DataNascimento
                                                  ,DataInicio
                                                  ,DataRenovacao
                                                  ,DescricaoPaciente
                                                  ,Observacao
                                                  ,Particulariedade
                                                  ,Jornada
                                                  ,DataCriacao
                                                  ,DataAlteracao
                                                  ,Ativo)
	                                        	   OUTPUT INSERTED.PacienteId
                                            VALUES
                                                  (@NomePaciente
                                                  ,@DataNascimento
                                                  ,@DataInicio
                                                  ,@DataRenovacao
                                                  ,@DescricaoPaciente
                                                  ,@Observacao
                                                  ,@Particulariedade
                                                  ,@Jornada
                                                  ,GETDATE()
                                                  ,GETDATE()
                                                  ,1)";

            var pacienteId = await _connection.ExecuteScalarAsync<int>(sql, paciente, commandType: CommandType.Text);

            foreach (var telefone in paciente.TelefonesPacientes)
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

                int telefoneId = await _connection.ExecuteScalarAsync<int>(sql_telefone, telefone, commandType: CommandType.Text);


                const string sql_paciente_telefone = @"INSERT INTO Paciente_Telefones
                                                                           (PacienteId
                                                                           ,TelefoneId
                                                                           ,DataCriacao
                                                                           ,DataAlteracao)
                                                                     VALUES
                                                                           (@PacienteId
                                                                           ,@TelefoneId
                                                                           ,GETDATE()
                                                                           ,GETDATE())";

                await _connection.ExecuteScalarAsync<int>(sql_paciente_telefone, new { pacienteId, telefoneId }, commandType: CommandType.Text);

            }

            foreach (var endereco in paciente.EnderecosPaciente)
            {
                const string sql_endereco = @"INSERT INTO Enderecos
                                                                 (Logradouro
                                                                 ,Numero
                                                                 ,Complemento
                                                                 ,Cep
                                                                 ,Bairro
                                                                 ,DataCriacao
                                                                 ,DataAlteracao
                                                                 ,Ativo)
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


                const string sql_paciente_endereco = @"INSERT INTO Paciente_Enderecos
                                                                              (PacienteId
                                                                              ,EnderecoId
                                                                              ,DataCriacao
                                                                              ,DataAlteracao
                                                                              ,Ativo)
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
                const string sql_responsavel = @"INSERT INTO Responsaveis
                                                                  (PacienteId
                                                                  ,NomeResponsavel
                                                                  ,DataCriacao
                                                                  ,DataAlteracao
                                                                  ,Ativo)
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
                    const string sql_telefone_responsavel = @"INSERT Telefones
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


                    const string sql_paciente_telefone = @"INSERT INTO Responsavel_Telefones
                                                                                      (TelefoneId
                                                                                      ,ResponsavelId
                                                                                      ,DataCriacao
                                                                                      ,DataAlteracao)
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
                const string sql_atendimento = @"INSERT INTO Atendimentos
                                                                   (PacienteId
                                                                   ,CuidadorId
                                                                   ,TurnoId
                                                                   ,ProfissionalCor
                                                                   ,DataInicio
                                                                   ,DataCriacao
                                                                   ,DataAlteracao
                                                                   ,Ativo)
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
                const string sql_paciente_pacote = @"INSERT INTO Paciente_Pacotes
                                                                     (PacienteId
                                                                      ,PacoteId
                                                                      ,PacoteMensal
                                                                      ,ValorPacote
                                                                      ,DiaPlantao
                                                                      ,ValorPlantaoPacote
                                                                      ,SalarioCuidador
                                                                      ,SalarioDiaCuidador
                                                                      ,ValorPlantaoCuidador
                                                                      ,Observacao
                                                                      ,ValorDesconto
                                                                      ,ValorAcrescimo
                                                                      ,TaxaAdminstrativa
                                                                      ,DataCriacao
                                                                      ,DataAlteracao
                                                                      ,Ativo)
                                                                 VALUES
                                                                       (@PacienteId
                                                                       ,@PacoteId
                                                                       ,@PacoteMensal
                                                                       ,@ValorPacote
                                                                       ,@DiaPlantao
                                                                       ,@ValorPlantaoPacote
                                                                       ,@SalarioCuidador
                                                                       ,@SalarioDiaCuidador
                                                                       ,@ValorPlantaoCuidador
                                                                       ,@Observacao
                                                                       ,@ValorDesconto
                                                                       ,@ValorAcrescimo
                                                                       ,@TaxaAdminstrativa
                                                                       ,GETDATE()
                                                                       ,GETDATE()
                                                                       ,1)";

                await _connection.ExecuteScalarAsync<int>(sql_paciente_pacote, paciente_Pacote, commandType: CommandType.Text);
            }

            return pacienteId;
        }
        public async Task<int> UpdatePaciente(Paciente paciente)
        {

            const string sql_Paciente = @"UPDATE Pacientes
                                   SET NomePaciente = @NomePaciente
                                      ,DataNascimento = @DataNascimento
                                      ,DataInicio = @DataInicio
                                      ,DataRenovacao = @DataRenovacao
                                      ,DescricaoPaciente = @DescricaoPaciente
                                      ,Observacao = @Observacao
                                      ,Particulariedade = @Particulariedade
                                      ,Jornada = @Jornada      
                                      ,DataAlteracao = GETDATE()
                                      ,Ativo = @Ativo
                                 WHERE PacienteId = @PacienteId";

            await _connection.ExecuteScalarAsync<int>(sql_Paciente, paciente, commandType: CommandType.Text);


            const string sql_telefoneId = @"SELECT	TelefoneId FROM Paciente_Telefones WHERE PacienteId = @PacienteId";


            var listTelefoneId = await _connection.QueryAsync<int>(sql_telefoneId, new { paciente.PacienteId}, commandType: CommandType.Text);

            const string sql_delete_paciente_telefone = @"DELETE FROM Paciente_Telefones WHERE PacienteId = @PacienteId";

            var id = await _connection.ExecuteScalarAsync<int>(sql_delete_paciente_telefone, new { paciente.PacienteId }, commandType: CommandType.Text);


            foreach (var telefoneId in listTelefoneId)
            {
                const string sql_delete_telefone = @"DELETE FROM Telefones WHERE TelefoneId = @TelefoneId";
                await _connection.ExecuteScalarAsync<int>(sql_delete_telefone, new { telefoneId }, commandType: CommandType.Text);
            }

            const string sql_enderecoId = @"SELECT EnderecoId FROM Paciente_Enderecos WHERE PacienteId = @PacienteId";


            var listEndereco = await _connection.QueryAsync<int>(sql_enderecoId, new { paciente.PacienteId }, commandType: CommandType.Text);

            const string sql_delete_paciente_endereco = @"DELETE FROM Paciente_Enderecos WHERE PacienteId = @PacienteId";

            var paciente_endereco_id = await _connection.ExecuteScalarAsync<int>(sql_delete_paciente_endereco, new { paciente.PacienteId }, commandType: CommandType.Text);


            foreach (var enderecoId in listEndereco)
            {
                const string sql_delete_endereco = @"DELETE FROM Enderecos WHERE EnderecoId = @EnderecoId";
                await _connection.ExecuteScalarAsync<int>(sql_delete_endereco, new { enderecoId }, commandType: CommandType.Text);
            }

            const string sql_responsavelId = @"SELECT ResponsavelId FROM Responsaveis WHERE PacienteId = @PacienteId";


            var listaresponsavelId = await _connection.QueryAsync<int>(sql_responsavelId, new { paciente.PacienteId }, commandType: CommandType.Text);

            foreach (var responsavel_Id in listaresponsavelId)
            {                

                const string sql_responsavel_telefone_Id = @"SELECT TelefoneId  FROM Responsavel_Telefones WHERE ResponsavelId = @responsavel_Id";


                var listresponsavel_telefonesId = await _connection.QueryAsync<int>(sql_responsavel_telefone_Id, new { responsavel_Id }, commandType: CommandType.Text);


                const string sql_delete_responsavel_telefone = @"DELETE FROM Responsavel_Telefones WHERE ResponsavelId = @responsavel_Id";

                await _connection.ExecuteScalarAsync<int>(sql_delete_responsavel_telefone, new { responsavel_Id }, commandType: CommandType.Text);

                foreach (var endereco_telefone_Id in listresponsavel_telefonesId)
                {
                    const string sql_delete_telefone_responsavel = @"DELETE FROM Telefones WHERE TelefoneId = @endereco_telefone_Id";
                    await _connection.ExecuteScalarAsync<int>(sql_delete_telefone_responsavel, new { endereco_telefone_Id }, commandType: CommandType.Text);
                }

                const string sql_delete_responsavel = @"DELETE FROM Responsaveis WHERE ResponsavelId = @responsavel_Id";
                await _connection.ExecuteScalarAsync<int>(sql_delete_responsavel, new { responsavel_Id }, commandType: CommandType.Text);
            }

           
            const string sql_delete_paciente_pacote = @"DELETE FROM Paciente_Pacotes WHERE PacienteId = @PacienteId";

            var paciente_pacote_id = await _connection.ExecuteScalarAsync<int>(sql_delete_paciente_pacote, new { paciente.PacienteId }, commandType: CommandType.Text);

            const string sql_delete_atendimento = @"DELETE FROM Atendimentos WHERE PacienteId = @PacienteId";

            var paciente_atendiemento_id = await _connection.ExecuteScalarAsync<int>(sql_delete_atendimento, new { paciente.PacienteId }, commandType: CommandType.Text);


            foreach (var telefone in paciente.TelefonesPacientes)
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

                int telefoneId = await _connection.ExecuteScalarAsync<int>(sql_telefone, telefone, commandType: CommandType.Text);


                const string sql_paciente_telefone = @"INSERT INTO Paciente_Telefones
                                                                           (PacienteId
                                                                           ,TelefoneId
                                                                           ,DataCriacao
                                                                           ,DataAlteracao)
                                                                     VALUES
                                                                           (@PacienteId
                                                                           ,@TelefoneId
                                                                           ,GETDATE()
                                                                           ,GETDATE())";

                await _connection.ExecuteScalarAsync<int>(sql_paciente_telefone, new { paciente.PacienteId, telefoneId }, commandType: CommandType.Text);

            }

            foreach (var endereco in paciente.EnderecosPaciente)
            {
                const string sql_endereco = @"INSERT INTO Enderecos
                                                                 (Logradouro
                                                                 ,Numero
                                                                 ,Complemento
                                                                 ,Cep
                                                                 ,Bairro
                                                                 ,DataCriacao
                                                                 ,DataAlteracao
                                                                 ,Ativo)
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


                const string sql_paciente_endereco = @"INSERT INTO Paciente_Enderecos
                                                                              (PacienteId
                                                                              ,EnderecoId
                                                                              ,DataCriacao
                                                                              ,DataAlteracao
                                                                              ,Ativo)
                                                                        VALUES
                                                                              (@PacienteId
                                                                              ,@EnderecoId
                                                                              ,GETDATE()
                                                                              ,GETDATE()
                                                                              ,1)";

                await _connection.ExecuteScalarAsync<int>(sql_paciente_endereco, new { paciente.PacienteId, enderecoId }, commandType: CommandType.Text);
            }

            foreach (var responsavel in paciente.RespensaveisPacientes)
            {
                var nomeResponsavel = responsavel.NomeResponsavel;
                const string sql_responsavel = @"INSERT INTO Responsaveis
                                                                  (PacienteId
                                                                  ,NomeResponsavel
                                                                  ,DataCriacao
                                                                  ,DataAlteracao
                                                                  ,Ativo)
                                                            OUTPUT INSERTED.ResponsavelId
                                                            VALUES
                                                                  (@PacienteId 
                                                                  ,@NomeResponsavel
                                                                  ,GETDATE()
                                                                  ,GETDATE()
                                                                  ,1)";

                int responsavelId = await _connection.ExecuteScalarAsync<int>(sql_responsavel, new { paciente.PacienteId, nomeResponsavel }, commandType: CommandType.Text);


                foreach (var telefoneresponsavel in responsavel.TelefonesResponsaveis)
                {
                    const string sql_telefone_responsavel = @"INSERT Telefones
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


                    const string sql_paciente_telefone = @"INSERT INTO Responsavel_Telefones
                                                                                      (TelefoneId
                                                                                      ,ResponsavelId
                                                                                      ,DataCriacao
                                                                                      ,DataAlteracao)
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
                atendimento.PacienteId = paciente.PacienteId;
                const string sql_atendimento = @"INSERT INTO Atendimentos
                                                                   (PacienteId
                                                                   ,CuidadorId
                                                                   ,TurnoId
                                                                   ,ProfissionalCor
                                                                   ,DataInicio
                                                                   ,DataCriacao
                                                                   ,DataAlteracao
                                                                   ,Ativo)
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
                paciente_Pacote.PacienteId = paciente.PacienteId;
                const string sql_paciente_pacote = @"INSERT INTO Paciente_Pacotes
                                                                     (PacienteId
                                                                      ,PacoteId
                                                                      ,PacoteMensal
                                                                      ,ValorPacote
                                                                      ,DiaPlantao
                                                                      ,ValorPlantaoPacote
                                                                      ,SalarioCuidador
                                                                      ,SalarioDiaCuidador
                                                                      ,ValorPlantaoCuidador
                                                                      ,Observacao
                                                                      ,ValorDesconto
                                                                      ,ValorAcrescimo
                                                                      ,TaxaAdminstrativa
                                                                      ,DataCriacao
                                                                      ,DataAlteracao
                                                                      ,Ativo)
                                                                 VALUES
                                                                       (@PacienteId
                                                                       ,@PacoteId
                                                                       ,@PacoteMensal
                                                                       ,@ValorPacote
                                                                       ,@DiaPlantao
                                                                       ,@ValorPlantaoPacote
                                                                       ,@SalarioCuidador
                                                                       ,@SalarioDiaCuidador
                                                                       ,@ValorPlantaoCuidador
                                                                       ,@Observacao
                                                                       ,@ValorDesconto
                                                                       ,@ValorAcrescimo
                                                                       ,@TaxaAdminstrativa
                                                                       ,GETDATE()
                                                                       ,GETDATE()
                                                                       ,1)";

                await _connection.ExecuteScalarAsync<int>(sql_paciente_pacote, paciente_Pacote, commandType: CommandType.Text);
            }

            return paciente.PacienteId;
        }
        public async Task<bool> DeletePacientePorIdAsync(int pacienteId)
        {
           

            const string sql_telefoneId = @"SELECT	TelefoneId FROM Paciente_Telefones WHERE PacienteId = @PacienteId";


            var listTelefoneId = await _connection.QueryAsync<int>(sql_telefoneId, new { pacienteId }, commandType: CommandType.Text);

            const string sql_delete_paciente_telefone = @"DELETE FROM Paciente_Telefones WHERE PacienteId = @PacienteId";

            var id = await _connection.ExecuteScalarAsync<int>(sql_delete_paciente_telefone, new { pacienteId }, commandType: CommandType.Text);


            foreach (var telefoneId in listTelefoneId)
            {
                const string sql_delete_telefone = @"DELETE FROM Telefones WHERE TelefoneId = @TelefoneId";
                await _connection.ExecuteScalarAsync<int>(sql_delete_telefone, new { telefoneId }, commandType: CommandType.Text);
            }

            const string sql_enderecoId = @"SELECT EnderecoId FROM Paciente_Enderecos WHERE PacienteId = @PacienteId";


            var listEndereco = await _connection.QueryAsync<int>(sql_enderecoId, new { pacienteId }, commandType: CommandType.Text);

            const string sql_delete_paciente_endereco = @"DELETE FROM Paciente_Enderecos WHERE PacienteId = @PacienteId";

            var paciente_endereco_id = await _connection.ExecuteScalarAsync<int>(sql_delete_paciente_endereco, new { pacienteId }, commandType: CommandType.Text);


            foreach (var enderecoId in listEndereco)
            {
                const string sql_delete_endereco = @"DELETE FROM Enderecos WHERE EnderecoId = @EnderecoId";
                await _connection.ExecuteScalarAsync<int>(sql_delete_endereco, new { enderecoId }, commandType: CommandType.Text);
            }

            const string sql_responsavelId = @"SELECT ResponsavelId FROM Responsaveis WHERE PacienteId = @PacienteId";


            var listaresponsavelId = await _connection.QueryAsync<int>(sql_responsavelId, new { pacienteId }, commandType: CommandType.Text);

            foreach (var responsavel_Id in listaresponsavelId)
            {

                const string sql_responsavel_telefone_Id = @"SELECT TelefoneId  FROM Responsavel_Telefones WHERE ResponsavelId = @responsavel_Id";


                var listresponsavel_telefonesId = await _connection.QueryAsync<int>(sql_responsavel_telefone_Id, new { responsavel_Id }, commandType: CommandType.Text);


                const string sql_delete_responsavel_telefone = @"DELETE FROM Responsavel_Telefones WHERE ResponsavelId = @responsavel_Id";

                await _connection.ExecuteScalarAsync<int>(sql_delete_responsavel_telefone, new { responsavel_Id }, commandType: CommandType.Text);

                foreach (var endereco_telefone_Id in listresponsavel_telefonesId)
                {
                    const string sql_delete_telefone_responsavel = @"DELETE FROM Telefones WHERE TelefoneId = @endereco_telefone_Id";
                    await _connection.ExecuteScalarAsync<int>(sql_delete_telefone_responsavel, new { endereco_telefone_Id }, commandType: CommandType.Text);
                }

                const string sql_delete_responsavel = @"DELETE FROM Responsaveis WHERE ResponsavelId = @responsavel_Id";
                await _connection.ExecuteScalarAsync<int>(sql_delete_responsavel, new { responsavel_Id }, commandType: CommandType.Text);
            }


            const string sql_delete_paciente_pacote = @"DELETE FROM Paciente_Pacotes WHERE PacienteId = @PacienteId";

            var paciente_pacote_id = await _connection.ExecuteScalarAsync<int>(sql_delete_paciente_pacote, new { pacienteId }, commandType: CommandType.Text);

            const string sql_delete_atendimento = @"DELETE FROM Atendimentos WHERE PacienteId = @PacienteId";

            var paciente_atendiemento_id = await _connection.ExecuteScalarAsync<int>(sql_delete_atendimento, new { pacienteId }, commandType: CommandType.Text);

            const string sql_delete_paciente = @"DELETE FROM Pacientes WHERE PacienteId = @PacienteId";


             await _connection.QueryAsync<int>(sql_delete_paciente, new { pacienteId }, commandType: CommandType.Text);

            return true;

        }
        public async Task<bool> VerificaPacienteAsync(string nomePaciente)
        {
            const string sql = @"SELECT COUNT(a.NomePaciente)
                                    FROM Pacientes AS a  
                                    WHERE EXISTS  
                                    (SELECT a.NomePaciente FROM Pacientes AS b WHERE a.PacienteId = b.PacienteId AND replace(a.NomePaciente, ' ', '') = replace(@NomePaciente, ' ', ''));";
            var qtd = await _connection.ExecuteScalarAsync<int>(sql, new { nomePaciente }, commandType: CommandType.Text);
            if (qtd == 0) return false;
            return true;
        }
    }
}
