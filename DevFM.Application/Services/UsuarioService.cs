using DevFM.Application.Services.Util;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using DevFM.Domain.ViewModels;

namespace DevFM.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioSqlReadAdapter _usuarioSqlAdapter;

        public UsuarioService(IUsuarioSqlReadAdapter UsuarioSqlAdapter)
        {
            _usuarioSqlAdapter = UsuarioSqlAdapter;
        }

        public async Task<IEnumerable<Usuario>> ObterUsuarioAsync()
        {
            return await _usuarioSqlAdapter.ObterUsuarioAsync();
        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(int UsuarioId)
        {
            return await _usuarioSqlAdapter.ObterUsuarioPorIdAsync(UsuarioId);
        }

        public async Task<int> NewUsuarioAsync(Usuario Usuario)
        {
            Usuario.Senha = Recursos.ObterHashMD5(Usuario.Senha);


            return await _usuarioSqlAdapter.NewUsuarioAsync(Usuario);
        }

        public async Task<UsuarioLogadoVM> LoginUsuario(string login, string senha)
        {
            string senhaMD5 = Recursos.ObterHashMD5(senha);

            Usuario usuario = await _usuarioSqlAdapter.ObterPorUsuarioSenhaAsync(login, senhaMD5);
            usuario.Perfil = await _usuarioSqlAdapter.ObterPerfioPorIdAsync(usuario.PerfilId);


            if (usuario != null)
            {
                UsuarioLogadoVM usuarioLogado = new UsuarioLogadoVM()
                {
                    UsuarioId = usuario.UsuarioId,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    PerfilId = usuario.PerfilId,
                    DescricaoPerfil = usuario.Perfil.Descricao
                };
                return usuarioLogado;
            }

            return null;
            
        }
        public async Task<int> VerificaUsuarioAsync(string email)
        {
            return await _usuarioSqlAdapter.VerificaUsuarioAsync(email);
        }

    }
}
