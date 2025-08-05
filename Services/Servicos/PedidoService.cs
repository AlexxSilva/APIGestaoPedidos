using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Dto;
using APIGestaoPedidos.Dto.DtoPedido;
using APIGestaoPedidos.Infraestruture.Context;
using APIGestaoPedidos.Infraestruture.Interfaces;
using APIGestaoPedidos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIGestaoPedidos.Services.Servicos
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PedidoDto>> ListarTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task<PedidoDto?> BuscarPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task<Pedido> CriarPedidoAsync(Pedido pedido)
        {
            return await _repository.CriarPedidoAsync(pedido);
        }

        public async Task AprovarPedidoAsync(int id)
        {
            await _repository.AprovarPedidoAsync(id);
        }
    }
}
