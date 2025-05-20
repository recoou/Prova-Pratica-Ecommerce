using Ecommerce.Models;

namespace Ecommerce.Persistencia.Intefaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<Produto> AddAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task DeleteAsync(int id);
        Task<List<Produto>> GetAllFiltered();
    }
}
