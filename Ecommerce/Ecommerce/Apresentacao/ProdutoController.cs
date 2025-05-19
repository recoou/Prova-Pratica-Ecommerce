using Ecommerce.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Apresentacao
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        static private List<Produto> produtos = new List<Produto>
        {
            new Produto
            {
                Id = 1,
                Nome = "Box",
                Categoria = "Papel",
                Preco = 2.3,
                Status = true,
                ImagemCaminho = null

            },
            new Produto
            {
                Id = 2,
                Nome = "GoW Ascencion",
                Categoria = "Game",
                Preco = 200.99,
                Status = true,
                ImagemCaminho = null

            },
            new Produto
            {
                Id = 3,
                Nome = "Commander Starter Deck",
                Categoria = "Magic",
                Preco = 500,
                Status = false,
                ImagemCaminho = null

            }
        };

        [HttpGet]
        public ActionResult<List<Produto>> GetProdutos()
        {
            return Ok(produtos);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Produto> GetProdutoById(int id)
        {
            var produto = produtos.FirstOrDefault(g => g.Id == id);
            if(produto is null)
            {
                return NotFound("Produto não existe");
            }
             
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult<Produto> AddProduto(Produto novoProduto)
        {
            if (novoProduto is null)
            {
                return BadRequest();
            }

            novoProduto.Id = produtos.Max(g => g.Id) + 1;
            produtos.Add(novoProduto);
            return CreatedAtAction(nameof(GetProdutoById), new {id = novoProduto.Id}, novoProduto);

        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateProduto(int id, Produto novoProduto)
        {
            var produto = produtos.FirstOrDefault(g => g.Id == id);
            if (produto is null)
            {
                return BadRequest("Produto não existe");
            }

            produto.Nome = novoProduto.Nome;
            produto.Preco = novoProduto.Preco;
            produto.Categoria = novoProduto.Categoria;

            return NoContent();
            
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteProduto(int id)
        {
            var produto = produtos.FirstOrDefault(g => g.Id == id);
            if (produto is null)
            {
                return BadRequest("Produto não existe");
            }

            produtos.Remove(produto);
            return NoContent();

        }
    }
}
