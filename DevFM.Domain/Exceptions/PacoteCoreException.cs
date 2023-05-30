using DevFM.Domain.Models;

namespace DevFM.Domain.Exceptions
{

    public class PacoteCoreException : CoreException<PacoteCoreError>
    {
        public PacoteCoreException(PacoteCoreError PacoteCoreError) => AddError(PacoteCoreError);

        public override string Key => "PacoteCoreException";
    }

    public class PacoteCoreError : CoreError
    {
        private PacoteCoreError(string key, string message) : base(key, message)
        {
        }

        //429
        public static PacoteCoreError LimitRequestTrigged =>
            new PacoteCoreError("Limite de requisições",
                "O limite de requisições foi alcançado, Tente novamente mais tarde. ");

        //401
        public static PacoteCoreError UnauthorizedAccess =>
            new PacoteCoreError("Acesso não autorizado",
                "Ocorreu um problema na autorização.");

        //404
        public static PacoteCoreError NotFound =>
            new PacoteCoreError("Não encontrado",
                "Não existem Pacote com esse parâmtro.");
    }
}
