using APIGestaoPedidos.Domain.Entidades;

namespace APIGestaoPedidos.Infraestruture.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido?> ObterPorIdAsync(int id);
        Task<List<Pedido>> ObterTodosAsync();
        Task CriarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
    }
}
