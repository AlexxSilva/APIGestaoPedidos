using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Dto.DtoProduto;
using APIGestaoPedidos.Infraestruture.Context;
using APIGestaoPedidos.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIGestaoPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var produtos = await _service.ObterTodosProdutosAsync();
            var dto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var produto = await _service.ObterProdutoPorIdAsync(id);
            if (produto == null) return NotFound();

            var dto = _mapper.Map<ProdutoDto>(produto);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarProdutoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var produto = _mapper.Map<Produto>(dto);
            await _service.AdicionarProdutoAsync(produto);

            var retorno = _mapper.Map<ProdutoDto>(produto);
            return CreatedAtAction(nameof(GetPorId), new { id = produto.Id }, retorno);
        }
    }
}
