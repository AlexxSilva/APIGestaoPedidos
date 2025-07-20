namespace APIGestaoPedidos.Dto
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public string ClienteNome { get; set; }
        public int ClienteId { get; set; }
        public int Status { get; set; }
        public List<PedidoItemDto> Itens { get; set; }
    }
}
