using APIGestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIGestaoPedidos.Domain.Entidades
{
    public class PedidoItem
    {
        [Key]
        public int Id { get; private set; }

        public int PedidoId { get; private set; }
        [JsonIgnore]
        public Pedido? Pedido { get; set; } 
        public int ProdutoId { get; set; }

        [JsonIgnore]
        public Produto? Produto { get; set; }

        [Precision(18, 4)]
        public decimal ValorUnitario { get; set; }

    }
}
