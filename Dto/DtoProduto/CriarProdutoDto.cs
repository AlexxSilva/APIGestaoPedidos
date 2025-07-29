using System.ComponentModel.DataAnnotations;

namespace APIGestaoPedidos.Dto.DtoProduto
{
    public class CriarProdutoDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Unidade { get; set; } = string.Empty;
    }
}
