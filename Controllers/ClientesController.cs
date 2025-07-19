using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Infraestruture.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIGestaoPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPorId), new { id = cliente.Id }, cliente);
        }
    }

}
