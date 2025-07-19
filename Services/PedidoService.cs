using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Infraestruture.Context;
using Microsoft.EntityFrameworkCore;

namespace APIGestaoPedidos.Services
{
    public class PedidoService
    {
        private readonly AppDbContext _context;

        public PedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> ListarTodosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .ToListAsync();
        }

        public async Task<Pedido?> BuscarPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> CriarPedidoAsync(Pedido pedido)
        {
            if (pedido.Itens == null || pedido.Itens.Count == 0)
                throw new ArgumentException("Pedido deve conter ao menos um item.");

            if (pedido.Cliente == null)
                throw new ArgumentException("Cliente deve ser informado.");

            var cliente = await _context.Clientes.FindAsync(pedido.Cliente.Id);
            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado.");

            foreach (var item in pedido.Itens)
            {
                var produto = await _context.Produtos.FindAsync(item.Produto.Id);
                if (produto == null)
                    throw new ArgumentException($"Produto com ID {item.Produto.Id} não encontrado.");
                item.Produto = produto;
                item.ValorUnitario = produto.Estoque.Length; // Exemplo, ajuste pra lógica real
            }

            pedido = new Pedido(); // pode construir com um construtor
            pedido.AdicionarItens(pedido.Itens);
            pedido.DefinirCliente(cliente);

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task AprovarPedidoAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                throw new InvalidOperationException("Pedido não encontrado.");

            pedido.Aprovar(); // Método de domínio que altera o Status
            await _context.SaveChangesAsync();
        }
    }
}
