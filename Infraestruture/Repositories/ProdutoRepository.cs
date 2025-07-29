using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Infraestruture.Context;
using APIGestaoPedidos.Infraestruture.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIGestaoPedidos.Infraestruture.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProdutoRepository(AppDbContext appDbContext)
        {
           _appDbContext = appDbContext;
        }
        public async Task AdicionarProdutoAsync(Produto produto)
        {
            await _appDbContext.Produtos.AddAsync(produto);
            await _appDbContext.SaveChangesAsync();
            
        }

        public async Task<Produto> ObterProdutosPorIdAsync(int id)  => await _appDbContext.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<Produto>> ObterTodosProdutosAsync()=> await _appDbContext.Produtos.ToListAsync();
    }
}
