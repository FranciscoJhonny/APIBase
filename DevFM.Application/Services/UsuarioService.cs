using DevFM.Application.Services.Util;
using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioSqlReadAdapter _UsuarioSqlAdapter;

        public UsuarioService(IUsuarioSqlReadAdapter UsuarioSqlAdapter)
        {
            _UsuarioSqlAdapter = UsuarioSqlAdapter;
        }

        public async Task<IEnumerable<Usuario>> ObterUsuarioAsync()
        {
            return await _UsuarioSqlAdapter.ObterUsuarioAsync();
        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(int UsuarioId)
        {
            return await _UsuarioSqlAdapter.ObterUsuarioPorIdAsync(UsuarioId);
        }

        public async Task<int> NewUsuarioAsync(Usuario Usuario)
        {
            Usuario.Senha = Recursos.ObterHashMD5(Usuario.Senha);


            return await _UsuarioSqlAdapter.NewUsuarioAsync(Usuario);
        }

    }
}
