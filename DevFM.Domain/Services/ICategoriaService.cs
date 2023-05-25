using DevFM.Domain.Models;

namespace DevFM.Domain.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ObterCategoriaAsync();
        Task<Categoria> ObterCategoriaPorIdAsync(int categoriaId);
    }
}
