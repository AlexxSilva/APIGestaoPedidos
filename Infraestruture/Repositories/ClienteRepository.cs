using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Infraestruture.Context;
using APIGestaoPedidos.Infraestruture.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIGestaoPedidos.Infraestruture.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarClienteAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> ObterClientePorIdAsync(int id) => await _context.Clientes.FirstOrDefaultAsync(p => p.Id == id);
        public async Task<List<Cliente>> ObterTodosClientesAsync() => await _context.Clientes.ToListAsync();
    }
}
