using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Dto.DtoPedido;

namespace APIGestaoPedidos.Infraestruture.Interfaces
{
    public interface IPedidoRepository
    {
        Task<PedidoDto?> ObterPorIdAsync(int id);
        Task<List<PedidoDto>> ObterTodosAsync();
        Task<Pedido> CriarPedidoAsync(Pedido pedido);
        public  Task AprovarPedidoAsync(int id);
    }
}
