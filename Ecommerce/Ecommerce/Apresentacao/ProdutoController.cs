using Ecommerce.Models;
using Ecommerce.Negocio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Apresentacao
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoServices _produtoServices;

        public ProdutoController(IProdutoServices produtoServices)
        {
            _produtoServices = produtoServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetProdutos()
        {
            var produtos = await _produtoServices.BuscarTodosProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
           var produto = await _produtoServices.BuscarProdutoPorIdAsync(id);

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> AddProduto(Produto novoProduto)
        {
            if (novoProduto is null)
            {
                return BadRequest();
            }

            novoProduto = await _produtoServices.AdicionarProdutoAsync(novoProduto);
            return CreatedAtAction(nameof(GetProdutos), new {id = novoProduto.Id}, novoProduto);

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Produto>> UpdateProduto(int id, Produto novoProduto)
        {
            if(id != novoProduto.Id)
            {
                return BadRequest("Id inconsistente");
            }

            var produto = await _produtoServices.BuscarProdutoPorIdAsync(novoProduto.Id);
            if (produto is null)
            {
                return BadRequest("Produto não existe");
            }

            produto = await _produtoServices.AtualizarProdutoAsync(novoProduto);

            return Ok(produto);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {

            await _produtoServices.ApagarProdutoAsync(id);

            return Ok("Produto foi apagado com sucesso.");

        }
        [HttpGet("filtrar")]
        public async Task<ActionResult<List<Produto>>> FiltrarProdutos(
            [FromQuery] string? categoria,
            [FromQuery] double? precoMenor,
            [FromQuery] double? precoMaior,
            [FromQuery] bool? status
            )
        {
            try
            {
                var produtos = await _produtoServices.BuscarProdutosFiltradosAsync(categoria, precoMenor, precoMaior, status);
                return Ok(produtos);
            }
            catch (Exception ex) 
            {
                return BadRequest($"Houve um erro ao chamar o filtro: {ex.Message}");
            }


        }
    }
}
