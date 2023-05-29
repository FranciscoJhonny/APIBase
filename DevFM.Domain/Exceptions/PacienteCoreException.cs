using DevFM.Domain.Models;

namespace DevFM.Domain.Exceptions
{

    public class PacienteCoreException : CoreException<PacienteCoreError>
    {
        public PacienteCoreException(PacienteCoreError PacienteCoreError) => AddError(PacienteCoreError);

        public override string Key => "PacienteCoreException";
    }

    public class PacienteCoreError : CoreError
    {
        private PacienteCoreError(string key, string message) : base(key, message)
        {
        }

        //429
        public static PacienteCoreError LimitRequestTrigged =>
            new PacienteCoreError("Limite de requisições",
                "O limite de requisições foi alcançado, Tente novamente mais tarde. ");

        //401
        public static PacienteCoreError UnauthorizedAccess =>
            new PacienteCoreError("Acesso não autorizado",
                "Ocorreu um problema na autorização.");

        //404
        public static PacienteCoreError NotFound =>
            new PacienteCoreError("Não encontrado",
                "Não existem paciente com esse parâmtro.");
    }
}
