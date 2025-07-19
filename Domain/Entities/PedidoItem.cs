using APIGestaoPedidos.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APIGestaoPedidos.Domain.Entidades
{
    public class PedidoItem
    {
        [Key]
        public int Id { get; set; }

        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; } 

        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public decimal ValorUnitario { get; set; }

    }
}
