using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Dto.DtoPedido;
using APIGestaoPedidos.Events;
using APIGestaoPedidos.Infraestruture.Context;
using APIGestaoPedidos.Infraestruture.Interfaces;
using APIGestaoPedidos.MessageBus.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace APIGestaoPedidos.Infraestruture.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly IMessageBus _messageBus;

        public PedidoRepository(AppDbContext context, IMessageBus messageBus)
        {
            _context = context;
            _messageBus = messageBus;
        }

        public async Task<PedidoDto?> ObterPorIdAsync(int id)
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
                          StatusDescricao = p.Status.ToString(),
                          Itens = p.Itens.Select(i => new PedidoItemDto
                          {
                              ProdutoId = i.ProdutoId,
                              ProdutoDescricao = i.Produto.Descricao,
                              ValorUnitario = i.ValorUnitario
                          }).ToList()
                      })
                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PedidoDto>> ObterTodosAsync()
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
             StatusDescricao = p.Status.ToString(),
             Itens = p.Itens.Select(i => new PedidoItemDto
             {
                 ProdutoId = i.ProdutoId,
                 ProdutoDescricao = i.Produto.Descricao,
                 ValorUnitario = i.ValorUnitario,
             }).ToList()
         })
         .ToListAsync();
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

            pedido.Aprovar(); 

            //publicar evento na fila
            var evento = new PedidoAprovadoEvent
            {
                PedidoId = pedido.Id,
                ClienteId = pedido.ClienteId,
                ValorTotal = pedido.Itens.Sum(i => i.ValorUnitario)
            };

            await _messageBus.PublicarAsync(evento, "pagamento-pedido-aprovado");

            await _context.SaveChangesAsync();
        }
    }
}
