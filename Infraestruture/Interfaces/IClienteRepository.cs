using APIGestaoPedidos.Domain.Entities;

namespace APIGestaoPedidos.Infraestruture.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObterTodosClientesAsync();
        Task<Cliente> ObterClientePorIdAsync(int Id);
        Task AdicionarClienteAsync(Cliente cliente);

    }
}
