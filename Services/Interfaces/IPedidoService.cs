using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Dto.DtoPedido;
using Microsoft.EntityFrameworkCore;

namespace APIGestaoPedidos.Services.Interfaces
{
    public interface IPedidoService
    {


        public Task<List<PedidoDto>> ListarTodosAsync();
        public  Task<PedidoDto?> BuscarPorIdAsync(int id);
        public  Task<Pedido> CriarPedidoAsync(Pedido pedido);
        public  Task AprovarPedidoAsync(int id);

    }
}
