using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Dto;
using APIGestaoPedidos.Dto.DtoPedido;
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

        public async Task<List<PedidoDto>> ListarTodosAsync()
        {
            return await _context.Pedidos
        .Include(p => p.Cliente)
        .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
        .Select(p => new PedidoDto
        {
            Id = p.Id,
            ClienteId = p.ClienteId,
            ClienteNome = p.Cliente.Nome,
            Status = (int)p.Status,
            Itens = p.Itens.Select(i => new PedidoItemDto
            {
                ProdutoId = i.ProdutoId,
                ProdutoDescricao = i.Produto.Descricao,
                ValorUnitario = i.ValorUnitario
            }).ToList()
        })
        .ToListAsync();
        }

        public async Task<PedidoDto?> BuscarPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                     .Select(p => new PedidoDto
                     {
                         Id = p.Id,
                         ClienteId = p.ClienteId,
                         ClienteNome = p.Cliente.Nome,
                         Status = (int)p.Status,
                         Itens = p.Itens.Select(i => new PedidoItemDto
                         {
                             ProdutoId = i.ProdutoId,
                             ProdutoDescricao = i.Produto.Descricao,
                             ValorUnitario = i.ValorUnitario
                         }).ToList()
                     })
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> CriarPedidoAsync(Pedido pedido)
        {
        
            //Valida se o cliente foi preenchido
            if (pedido.ClienteId == 0)
                throw new ArgumentException("Cliente deve ser informado.");

            //Valida se o cliente existe
            var cliente = await _context.Clientes.FindAsync(pedido.ClienteId);
            if (cliente == null)
                throw new ArgumentException("Cliente não encontrado.");


            //Verifica se adicionou itens ao pedido
            if (pedido.Itens == null || pedido.Itens.Count == 0)
                throw new ArgumentException("Pedido deve conter ao menos um item.");

            //verifica se os itens do pedido são validos
            foreach (var item in pedido.Itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto == null)
                    throw new ArgumentException($"Produto com ID {item.Produto.Id} não encontrado.");
                item.Produto = produto;
                item.ValorUnitario = produto.Estoque.Length; 
            }

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
