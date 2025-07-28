using System.ComponentModel.DataAnnotations;

namespace APIGestaoPedidos.Dto.DtoCliente
{
    public class CriarClienteDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter até 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
    }
}
