using DevFM.Domain.Models;

namespace DevFM.Domain.Adapters
{
    public interface ICategoriaSqlReadAdapter
    {
        Task<IEnumerable<Categoria>> ObterCategoriaAsync();
        Task<Categoria> ObterCategoriaPorIdAsync(int categoriaId);

    }
}
