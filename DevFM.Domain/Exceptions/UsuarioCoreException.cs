using DevFM.Domain.Models;

namespace DevFM.Domain.Exceptions
{

    public class UsuarioCoreException : CoreException<UsuarioCoreError>
    {
        public UsuarioCoreException(UsuarioCoreError UsuarioCoreError) => AddError(UsuarioCoreError);

        public override string Key => "UsuarioCoreException";
    }

    public class UsuarioCoreError : CoreError
    {
        private UsuarioCoreError(string key, string message) : base(key, message)
        {
        }

        //429
        public static UsuarioCoreError LimitRequestTrigged =>
            new UsuarioCoreError("Limite de requisições",
                "O limite de requisições foi alcançado, Tente novamente mais tarde. ");

        //401
        public static UsuarioCoreError UnauthorizedAccess =>
            new UsuarioCoreError("Acesso não autorizado",
                "Ocorreu um problema na autorização.");

        //404
        public static UsuarioCoreError NotFound =>
            new UsuarioCoreError("Não encontrado",
                "Não existem turma com esse parâmtro.");
    }
}
