using APIGestaoPedidos.Domain.Entities;
using APIGestaoPedidos.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIGestaoPedidos.Domain.Entidades
{
    public class Pedido
    {

        [Key]
        public int Id { get; private set; }

        public int ClienteId { get; set; }
        [JsonIgnore]
        public Cliente? Cliente { get; private set; }
        public StatusPedido Status { get;  set; } = StatusPedido.Pendente;
        public List<PedidoItem> Itens { get;  set; } = new();

        public void Aprovar()
        {
            if (Status != StatusPedido.Pendente)
                throw new InvalidOperationException("Pedido já foi processado.");

            Status = StatusPedido.Aprovado;
        }

        public void AdicionarItens(List<PedidoItem> itens)
        {
            if (itens == null || !itens.Any())
                throw new ArgumentException("Pedido deve conter itens.");

            Itens = itens;
        }

        public void DefinirCliente(Cliente cliente)
        {
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
        }

    }
}
