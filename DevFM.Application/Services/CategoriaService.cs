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
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaSqlReadAdapter _CategoriaSqlAdapter;

        public CategoriaService(ICategoriaSqlReadAdapter CategoriaSqlAdapter)
        {
            _CategoriaSqlAdapter = CategoriaSqlAdapter;
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriaAsync()
        {
            return await _CategoriaSqlAdapter.ObterCategoriaAsync();
        }

        public async Task<Categoria> ObterCategoriaPorIdAsync(int categoriaId)
        {
            return await _CategoriaSqlAdapter.ObterCategoriaPorIdAsync(categoriaId);
        }
    }
}
