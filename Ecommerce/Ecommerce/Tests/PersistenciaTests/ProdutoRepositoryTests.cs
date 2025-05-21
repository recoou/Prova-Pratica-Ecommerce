using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Persistencia;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ecommerce.Tests.PersistenciaTests
{
    public class ProdutoRepositoryTests
    {

        private readonly EcommerceDbContext _context;
        private readonly ProdutoRepository _repository;

        public ProdutoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseInMemoryDatabase(databaseName: "ProdutoDbTest")
                .Options;

            _context = new EcommerceDbContext(options);
            _repository = new ProdutoRepository(_context);
        }

        [Fact]
        public async Task Deve_Salvar_Novo_Produto()
        {
            var produto = new Produto
            {
                Nome = "Capa de Chuva",
                Categoria = "Roupa",
                Preco = 79.99,
                Status = true,
                ImagemCaminho = null
            };

            var resultado = await _repository.AddAsync(produto);

            Assert.NotNull(resultado);
            Assert.Equal("Capa de Chuva", resultado.Nome);
        }

        [Fact]
        public async Task Deve_Buscar_Um_Produto_Por_Id()
        {
            var produto = new Produto
            {
                Nome = "Capa de Chuva",
                Categoria = "Roupa",
                Preco = 79.99,
                Status = true,
                ImagemCaminho = null
            };

            var adicionado = await _repository.AddAsync(produto);

            var encontrado = await _repository.GetByIdAsync(adicionado.Id);

            Assert.NotNull(encontrado);
            Assert.Equal("Capa de Chuva", encontrado!.Nome);
        }

        [Fact]
        public async Task Deve_Atualizar_Produto()
        {
            var produto = new Produto
            {
                Nome = "Capa de Chuva",
                Categoria = "Roupa",
                Preco = 79.99,
                Status = true,
                ImagemCaminho = null
            };

            var salvo = await _repository.AddAsync(produto);
            salvo.Preco = 650;

            var atualizado = await _repository.UpdateAsync(salvo);

            Assert.NotNull(atualizado);
            Assert.Equal(650, atualizado!.Preco);
        }

        [Fact]
        public async Task Deve_Mudar_Status_Do_Produto_Para_False()
        {
            var produto = new Produto()
            {
                Nome = "Capa de Chuva",
                Categoria = "Roupa",
                Preco = 79.99,
                Status = true,
                ImagemCaminho = null
            };
            var salvo = await _repository.AddAsync(produto);
            var sucesso = await _repository.DeleteAsync(salvo.Id);
            var desativado = await _repository.GetByIdAsync(produto.Id);

            Assert.True(sucesso);
            Assert.Null(desativado);

            var noBanco = await _context.Produtos.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == salvo.Id);
            Assert.NotNull(noBanco);
            Assert.False(noBanco!.Status);

        }


    }
}
