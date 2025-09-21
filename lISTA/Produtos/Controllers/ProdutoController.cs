using Microsoft.AspNetCore.Mvc;
using Produtos.Models;
using Produtos.Models.Requests;

namespace Produtos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private static List<Produto> produtos = new List<Produto>();
        
        [HttpPost]
        public IActionResult CriarProduto([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (produtos.Any(p => p.CodigoProduto == produto.CodigoProduto))
            {
                return BadRequest("Código do produto já cadastrado no sistema");
            }
            
            produtos.Add(produto);
            return Ok(new { message = "Produto criado com sucesso", produto });
        }
        
        [HttpGet]
        public IActionResult ObterProdutos()
        {
            return Ok(produtos);
        }
        
        [HttpGet("{codigoProduto}")]
        public IActionResult ObterProduto(string codigoProduto)
        {
            var produto = produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            return Ok(produto);
        }
        
        [HttpPut("{codigoProduto}")]
        public IActionResult AtualizarProduto(string codigoProduto, [FromBody] Produto produtoAtualizado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var produto = produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            
            if (produtoAtualizado.CodigoProduto != codigoProduto && 
                produtos.Any(p => p.CodigoProduto == produtoAtualizado.CodigoProduto))
            {
                return BadRequest("Novo código do produto já cadastrado para outro produto");
            }
            
            produto.Descricao = produtoAtualizado.Descricao;
            produto.Preco = produtoAtualizado.Preco;
            produto.Estoque = produtoAtualizado.Estoque;
            produto.CodigoProduto = produtoAtualizado.CodigoProduto;
            
            return Ok(new { message = "Produto atualizado com sucesso", produto });
        }
        
        [HttpPatch("{codigoProduto}/estoque")]
        public IActionResult AtualizarEstoque(string codigoProduto, [FromBody] AtualizarEstoqueRequest request)
        {
            var produto = produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            
            if (request.NovoEstoque < 0)
            {
                return BadRequest("Estoque não pode ser negativo");
            }
            
            produto.Estoque = request.NovoEstoque;
            return Ok(new { message = "Estoque atualizado com sucesso", produto });
        }
        
        [HttpDelete("{codigoProduto}")]
        public IActionResult DeletarProduto(string codigoProduto)
        {
            var produto = produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            
            produtos.Remove(produto);
            return Ok(new { message = "Produto removido com sucesso" });
        }
        
        [HttpGet("buscar")]
        public IActionResult BuscarProdutos([FromQuery] string? descricao, 
                                           [FromQuery] decimal? precoMinimo, 
                                           [FromQuery] decimal? precoMaximo)
        {
            var query = produtos.AsQueryable();
            
            if (!string.IsNullOrEmpty(descricao))
            {
                query = query.Where(p => p.Descricao.Contains(descricao, StringComparison.OrdinalIgnoreCase));
            }
            
            if (precoMinimo.HasValue)
            {
                query = query.Where(p => p.Preco >= precoMinimo);
            }
            
            if (precoMaximo.HasValue)
            {
                query = query.Where(p => p.Preco <= precoMaximo);
            }
            
            return Ok(query.ToList());
        }
    }
}