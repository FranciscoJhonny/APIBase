using DevFM.Domain.Models;

namespace DevFM.Domain.Exceptions
{

    public class CategoriaCoreException : CoreException<CategoriaCoreError>
    {
        public CategoriaCoreException(CategoriaCoreError CategoriaCoreError) => AddError(CategoriaCoreError);

        public override string Key => "CategoriaCoreException";
    }

    public class CategoriaCoreError : CoreError
    {
        private CategoriaCoreError(string key, string message) : base(key, message)
        {
        }

        //429
        public static CategoriaCoreError LimitRequestTrigged =>
            new CategoriaCoreError("Limite de requisições",
                "O limite de requisições foi alcançado, Tente novamente mais tarde. ");

        //401
        public static CategoriaCoreError UnauthorizedAccess =>
            new CategoriaCoreError("Acesso não autorizado",
                "Ocorreu um problema na autorização.");

        //404
        public static CategoriaCoreError NotFound =>
            new CategoriaCoreError("Não encontrado",
                "Não existem turma com esse parâmtro.");
    }
}
