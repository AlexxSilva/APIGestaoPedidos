using APIGestaoPedidos.Domain.Entidades;
using APIGestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIGestaoPedidos.Infraestruture.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Produto> Produtos => Set<Produto>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Pedido → Itens
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens) //Um Pedido tem muitos Itens/p.Itens é a coleção de itens dentro da classe Pedido.
                .WithOne(i => i.Pedido) //Cada Item está relacionado a um Pedido. //i.Pedido é a propriedade de navegação dentro de ItemPedido.
                .HasForeignKey(i => i.PedidoId) //Define que a chave estrangeira (FK) no lado de Item será PedidoId.
                .OnDelete(DeleteBehavior.Cascade); //Cascade significa que todos os Itens relacionados também serão deletados automaticamente quando um Pedido for removido.

            // Pedido → Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany()//Cliente desvinculado do pedido
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // PedidoItem → Produto
            modelBuilder.Entity<PedidoItem>()
                .HasOne(i => i.Produto)
                .WithMany(p => p.PedidoItens)
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
