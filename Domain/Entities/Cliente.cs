using APIGestaoPedidos.Domain.Entidades;
using System.ComponentModel.DataAnnotations;

namespace APIGestaoPedidos.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public int Id { get; private set; }
        public string Nome { get;  set; } = string.Empty;
        // public List<Pedido> Pedidos { get; set; } = new(); Cliente independente, não precisa do pedido

    }
}
