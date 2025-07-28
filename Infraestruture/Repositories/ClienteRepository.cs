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
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> ObterClientePorIdAsync(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Cliente>> ObterTodosClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}
