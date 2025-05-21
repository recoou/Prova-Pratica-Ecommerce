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
            try
            {
                var produtos = await _produtoServices.BuscarTodosProdutosAsync();
                return Ok(produtos);
            }
            catch (Exception ex) { 
                return StatusCode(500, new {erro = "Erro inesperado", detalhes = ex.Message});
            }
            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
            try
            {
                var produto = await _produtoServices.BuscarProdutoPorIdAsync(id);
                if (produto == null)
                {
                    return NotFound("Produto com esse id não está cadastrado no banco");
                }

                return Ok(produto);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { erro = "Erro inesperado", detalhes = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> AddProduto(Produto novoProduto)
        {
            if (novoProduto is null)
            {
                return BadRequest();
            }

            try
            {
                novoProduto = await _produtoServices.AdicionarProdutoAsync(novoProduto);
                return CreatedAtAction(nameof(GetProdutos), new { id = novoProduto.Id }, novoProduto);
            }
            catch( Exception ex){
                return StatusCode(500, new { erro = "Erro inesperado", detalhes = ex.Message });
            }
            

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Produto>> UpdateProduto(int id, Produto novoProduto)
        {
            if(id != novoProduto.Id)
            {
                return BadRequest("Id inconsistente");
            }

            try
            {
                var produto = await _produtoServices.BuscarProdutoPorIdAsync(novoProduto.Id);
                if (produto is null)
                {
                    return BadRequest("Produto não existe");
                }

                produto = await _produtoServices.AtualizarProdutoAsync(novoProduto);

                return Ok(produto);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { erro = "Erro inesperado", detalhes = ex.Message });
            }
            

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                bool sucesso = await _produtoServices.ApagarProdutoAsync(id);
                if(!sucesso)
                    return NotFound("Produto não foi encontrado na base de dados.");

                return Ok("Produto foi apagado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro inesperado", detalhes = ex.Message });
            }
            

        }
        [HttpGet]
        [Route("filtrar")]
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
                return StatusCode(500, new { erro = "Erro inesperado", detalhes = ex.Message });
            }


        }
        [HttpPost]
        [Route("{id}/upload-imagem")]
        public async Task<IActionResult> UploadImagem(int id, IFormFile imagem)
        {
            if (imagem == null || imagem.Length == 0)
                return BadRequest("Houve algum erro com o envio da imagem.");

            try
            {
                var produto = await _produtoServices.BuscarProdutoPorIdAsync(id);
                if (produto == null)
                    return NotFound("Produto com esse id não está cadastrado na base de dados");

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imagem.FileName)}";
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var completePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(completePath, FileMode.Create))
                {
                    await imagem.CopyToAsync(stream);
                }

                produto.ImagemCaminho = $"/imagens/{fileName}";
                await _produtoServices.AtualizarProdutoAsync(produto);

                return Ok(new { imagem = produto.ImagemCaminho });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro inesperado", detalhes = ex.Message });
            }
        }
    }
}
