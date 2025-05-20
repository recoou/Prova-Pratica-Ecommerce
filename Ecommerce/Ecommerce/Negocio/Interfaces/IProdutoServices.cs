using Ecommerce.Models;

namespace Ecommerce.Negocio.Interfaces
{
    public interface IProdutoServices
    {
        Task<List<Produto>> BuscarTodosProdutosAsync();
        Task<Produto> BuscarProdutoPorIdAsync(int id);
        Task<Produto> AdicionarProdutoAsync(Produto produto);
        Task<Produto> AtualizarProdutoAsync(Produto produto);
        Task ApagarProdutoAsync(int id);
        Task<List<Produto>> BuscarProdutosFiltradosAsync();
    }
}
