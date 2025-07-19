using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Infraestruture.Context;
using APIGestaoPedidos.Infraestruture.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace APIGestaoPedidos.Infraestruture.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido?> ObterPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pedido>> ObterTodosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .ToListAsync();
        }

        public async Task CriarAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
