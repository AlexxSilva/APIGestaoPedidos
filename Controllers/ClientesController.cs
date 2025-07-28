using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Dto.DtoCliente;
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

    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;
        private readonly IMapper _mapper;

        public ClientesController(IClienteService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var clientes = await _service.ObterTodosClientesAsync();
            var dto = _mapper.Map<IEnumerable<ClienteDto>>(clientes);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var cliente = await _service.ObterClientePorIdAsync(id);
            if (cliente == null) return NotFound();

            var dto = new ClienteDto { Nome = cliente.Nome };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarClienteDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cliente = _mapper.Map<Cliente>(dto); // No cliente eu tenho o Id
            await _service.AdicionarClienteAsync(cliente);

            var retorno = _mapper.Map<ClienteDto>(cliente); // aqui não tem o ID

            return CreatedAtAction(nameof(GetPorId), new { id = cliente.Id }, retorno);
        }
    }

}
