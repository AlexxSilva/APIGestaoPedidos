using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Infraestruture.Context;
using APIGestaoPedidos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace APIGestaoPedidos.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        public PedidosController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // GET: api/pedidos

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var pedidos = await _pedidoService.ListarTodosAsync();
            return Ok(pedidos);
        }


        // GET: api/pedidos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var pedidos = await _pedidoService.ListarTodosAsync();
            return Ok(pedidos);
        }


        // POST: api/pedidos
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Pedido pedido)
        {
            if (pedido == null || pedido.Itens == null || pedido.Itens.Count == 0)
                return BadRequest("Pedido inválido.");

            await _pedidoService.CriarPedidoAsync(pedido);

            return CreatedAtAction(nameof(GetPorId), new { id = pedido.Id }, pedido);
        }

        // PUT: api/pedidos/{id}/aprovar
        [HttpPut("{id}/aprovar")]
        public async Task<IActionResult> Aprovar(int id)
        {
           await _pedidoService.AprovarPedidoAsync(id);
            return NoContent();
        }
    }
}
