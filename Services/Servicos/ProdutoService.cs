using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Infraestruture.Interfaces;
using APIGestaoPedidos.Services.Interfaces;

namespace APIGestaoPedidos.Services.Servicos
{
    public class ProdutoService : IProdutoService
    {
        IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task AdicionarProdutoAsync(Produto produto)
        {
            await _produtoRepository.AdicionarProdutoAsync(produto);
        }

        public async Task<Produto> ObterProdutoPorIdAsync(int Id) => await _produtoRepository.ObterProdutosPorIdAsync(Id);

        public async Task<List<Produto>> ObterTodosProdutosAsync() => await _produtoRepository.ObterTodosProdutosAsync();
  
    }
}
