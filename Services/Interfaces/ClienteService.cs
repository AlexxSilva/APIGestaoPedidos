using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Infraestruture.Interfaces;

namespace APIGestaoPedidos.Services.Interfaces
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task AdicionarClienteAsync(Cliente cliente) => await _repository.AdicionarClienteAsync(cliente);
        public async Task<Cliente> ObterClientePorIdAsync(int Id) => await _repository.ObterClientePorIdAsync(Id);
        public Task<List<Cliente>> ObterTodosClientesAsync() => _repository.ObterTodosClientesAsync();
    }
}
