using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Persistencia.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistencia
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly EcommerceDbContext _context;
        public ProdutoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            return await _context.Produtos.Where(p => p.Status == true).ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.Status == true);

            if (produto == null) {
                return null;
            }

            return produto;
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var produto = await GetByIdAsync(id);
            if (produto == null)
            {
                return false;
            }
            produto.Status = false;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<Produto?> UpdateAsync(Produto produto)
        {
            var produtoExistente = await GetByIdAsync(produto.Id);
            if (produtoExistente == null)
            {
                return null;
            }

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Categoria = produto.Categoria;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Status = produto.Status;

            await _context.SaveChangesAsync();

            return produtoExistente;

        }

        public async Task<List<Produto>> GetAllFiltered(string? categoria, double? precoMenor, double? precoMaior, bool? status)
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(p => p.Categoria.ToLower() == categoria.ToLower());
            if (precoMenor.HasValue)
                query = query.Where(p => p.Preco >= precoMenor.Value);
            if (precoMaior.HasValue)
                query = query.Where(p => p.Preco <= precoMaior.Value);
            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

            return await query.ToListAsync();
        }
    }
}
