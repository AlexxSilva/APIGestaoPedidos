using APIGestaoPedidos.Domain.Entidades;
using System.ComponentModel.DataAnnotations;

namespace APIGestaoPedidos.Domain.Entities
{
    public class Produto
    {
        [Key]
        public int Id { get; private set; } 
        public string Descricao { get ; set; } = string.Empty;
        public string Unidade { get; set; } = string.Empty;
        public string Estoque { get; set; } = string.Empty;
    }
}
