using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface IPacienteSqlReadAdapter
    {
        Task<IEnumerable<Paciente>> ObterPacienteAsync();
        Task<IEnumerable<Paciente>> ObterPacienteParametroAsync(int filtro, string nome);
        Task<Paciente> ObterPacientePorIdAsync(int pacienteId);
        Task<int> NewPacienteAsync(Paciente paciente);
        Task<IEnumerable<Telefone>> ObterTelefonesPacienteAsync(int pacienteId);
        Task<IEnumerable<Endereco>> ObterEnderecoPacienteAsync(int pacienteId);
        Task<IEnumerable<Responsavel>> ObterResponsavelPacienteAsync(int pacienteId);
        Task<IEnumerable<Telefone>> ObterTelefoneResponsavelAsync(int responsavelId);
        Task<IEnumerable<Atendimento>> ObterAtendimentoPacienteAsync(int pacienteId);
        Task<IEnumerable<Telefone>> ObterTelefoneCuidadorAsync(int cuidadorId);
        Task<IEnumerable<Paciente_Pacote>> ObterPaciente_PacoteAsync(int pacienteId);
        Task<int> UpdatePaciente(Paciente paciente);



    }
}
