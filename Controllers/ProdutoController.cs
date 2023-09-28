using FluentValidation;
using LojaDeGames.Model;
using LojaDeGames.Service;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeGames.Controllers
{
    [Route("~/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IValidator<Produto> _produtoValidator;
        public ProdutoController(
                IProdutoService produtoService,
                IValidator<Produto> ProdutoValidator
        )
        {
            _produtoService = produtoService;
            _produtoValidator = ProdutoValidator;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _produtoService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _produtoService.GetById(id);
            if (Resposta is null)
            {
                return NotFound();
            }
            return Ok(Resposta);
        }
        [HttpGet("nome/{nome}")]
        public async Task<ActionResult> GetByNome(string nome)
        {
            return Ok(await _produtoService.GetByNome(nome));
        }

        [HttpGet("nome/{nome}/console/{console}")]
        public async Task<ActionResult> GetByConsole(string nome, string console)
        {
            return Ok(await _produtoService.GetByNomeConsole(nome, console));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Produto produto)
        {
            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

            var Resposta = await _produtoService.Create(produto);

            if (Resposta is null)
                return BadRequest("Tema não encontrado");

            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Produto produto)
        {
            if (produto.Id == 0)
                return BadRequest("Id da Produto é invalida");

            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

            var Resposta = await _produtoService.Update(produto);

            if (Resposta is null)
                return NotFound("Produto e/ou Tema não encontrada!");
            return Ok(Resposta);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var BuscarProduto = await _produtoService.GetById(id);
            if (BuscarProduto is null)
                return NotFound("Produto não encontrada!");
            await _produtoService.Delete(BuscarProduto);
            return NoContent();
        }
        [HttpGet("preco/{valor1}/{valor2}")]
        public async Task<ActionResult> GetByPreco(decimal valor1, decimal valor2)
        {
            if (valor1 > valor2)
            {
                return BadRequest("O valor mínimo não pode ser maior que o valor máximo.");
            }

            var produtos = await _produtoService.GetByPreco(valor1, valor2);

            return Ok(produtos);
           
        }
    }
}
