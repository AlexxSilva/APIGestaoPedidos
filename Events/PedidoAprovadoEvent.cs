namespace APIGestaoPedidos.Events
{
    public class PedidoAprovadoEvent
    {
        public int PedidoId { get; set;}
        public int ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
    
        
    }
}
