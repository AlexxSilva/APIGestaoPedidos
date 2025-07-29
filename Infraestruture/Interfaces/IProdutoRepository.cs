using APIGestaoPedidos.Domain.Entities;

namespace APIGestaoPedidos.Infraestruture.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> ObterTodosProdutosAsync();
        Task<Produto> ObterProdutosPorIdAsync(int Id);
        Task AdicionarProdutoAsync(Produto produto);
    }
}
