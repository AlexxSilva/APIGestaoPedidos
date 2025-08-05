namespace APIGestaoPedidos.Dto.DtoPedido
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public string ClienteNome { get; set; }
        public int ClienteId { get; set; }
        public int Status { get; set; }
        public string StatusDescricao { get; set; } = string.Empty;
        public List<PedidoItemDto> Itens { get; set; }
    }
}
