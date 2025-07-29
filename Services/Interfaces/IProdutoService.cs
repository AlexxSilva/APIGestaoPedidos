using APIGestaoPedidos.Domain.Entities;

namespace APIGestaoPedidos.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<List<Produto>> ObterTodosProdutosAsync();
        Task<Produto> ObterProdutoPorIdAsync(int Id);
        Task AdicionarProdutoAsync(Produto produto);
    }
}
