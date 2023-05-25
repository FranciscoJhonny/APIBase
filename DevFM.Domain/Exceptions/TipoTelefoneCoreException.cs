using DevFM.Domain.Models;

namespace DevFM.Domain.Exceptions
{

    public class TipoTelefoneCoreException : CoreException<TipoTelefoneCoreError>
    {
        public TipoTelefoneCoreException(TipoTelefoneCoreError TipoTelefoneCoreError) => AddError(TipoTelefoneCoreError);

        public override string Key => "TipoTelefoneCoreException";
    }

    public class TipoTelefoneCoreError : CoreError
    {
        private TipoTelefoneCoreError(string key, string message) : base(key, message)
        {
        }

        //429
        public static TipoTelefoneCoreError LimitRequestTrigged =>
            new TipoTelefoneCoreError("Limite de requisições",
                "O limite de requisições foi alcançado, Tente novamente mais tarde. ");

        //401
        public static TipoTelefoneCoreError UnauthorizedAccess =>
            new TipoTelefoneCoreError("Acesso não autorizado",
                "Ocorreu um problema na autorização.");

        //404
        public static TipoTelefoneCoreError NotFound =>
            new TipoTelefoneCoreError("Não encontrado",
                "Não existem turma com esse parâmtro.");
    }
}
