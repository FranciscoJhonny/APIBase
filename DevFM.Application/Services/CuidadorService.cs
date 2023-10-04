using DevFM.Domain.Adapters;
using DevFM.Domain.Models;
using DevFM.Domain.Services;
using DevFM.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DevFM.Application.Services
{
    public class CuidadorService : ICuidadorService
    {
        private readonly ICuidadorSqlReadAdapter _cuidadorSqlAdapter;

        public CuidadorService(ICuidadorSqlReadAdapter CuidadorSqlAdapter)
        {
            _cuidadorSqlAdapter = CuidadorSqlAdapter;
        }

        public async Task<IEnumerable<Cuidador>> ObterCuidadorAsync()
        {
            return await _cuidadorSqlAdapter.ObterCuidadorAsync();
        }
        public async Task<IEnumerable<Cuidador>> ObterCuidadorNomeAsync(int filtro, string nome)
        {
            return await _cuidadorSqlAdapter.ObterCuidadorNomeAsync(filtro, nome);
        }

        public async Task<Cuidador> ObterCuidadorPorIdAsync(int cuidadorId)
        {
            return await _cuidadorSqlAdapter.ObterCuidadorPorIdAsync(cuidadorId);
        }

        public async Task<int> NewCuidadorAsync(Cuidador cuidador)
        {
            return await _cuidadorSqlAdapter.NewCuidadorAsync(cuidador);
        }

        public async Task<IEnumerable<Telefone>> ObterTelefonesCuidadorAsync(int cuidadorId)
        {
            return await _cuidadorSqlAdapter.ObterTelefonesCuidadorAsync(cuidadorId);
        }
        public async Task<int> UpdateCuidador(Cuidador cuidador)
        {
            return await _cuidadorSqlAdapter.UpdateCuidador(cuidador);
        }

        public async Task<bool> DeleteCuidadorPorIdAsync(int cuidadorId)
        {
            return await _cuidadorSqlAdapter.DeleteCuidadorPorIdAsync(cuidadorId);
        }

    }
}
