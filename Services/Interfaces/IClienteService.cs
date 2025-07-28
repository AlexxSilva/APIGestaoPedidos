using APIGestaoPedidos.Domain.Entities;

namespace APIGestaoPedidos.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObterTodosClientesAsync();
        Task<Cliente> ObterClientePorIdAsync(int Id);
        Task AdicionarClienteAsync(Cliente cliente);
    }
}
