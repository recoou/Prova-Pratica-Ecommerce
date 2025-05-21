using Ecommerce.Models;
using Ecommerce.Negocio.Interfaces;
using Ecommerce.Persistencia.Intefaces;

namespace Ecommerce.Negocio
{
    public class ProdutoServices: IProdutoServices
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoServices(IProdutoRepository produtoRepository) 
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<Produto>> BuscarTodosProdutosAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<Produto?> BuscarProdutoPorIdAsync(int id)
        {
            return await _produtoRepository.GetByIdAsync(id);
        }

        public async Task<List<Produto>> BuscarProdutosFiltradosAsync(string? categoria, double? precoMenor, double? precoMaior, bool? status)
        {
            return await _produtoRepository.GetAllFiltered(categoria, precoMenor, precoMaior, status);
        }

        public async Task<Produto> AdicionarProdutoAsync(Produto produto)
        {
            return await _produtoRepository.AddAsync(produto);
        }

        public async Task<bool> ApagarProdutoAsync(int id)
        {
            return await _produtoRepository.DeleteAsync(id);
        }

        public async Task<Produto?> AtualizarProdutoAsync(Produto produto)
        {
            return await _produtoRepository.UpdateAsync(produto);
        }
               
    }
}
