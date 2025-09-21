using System.ComponentModel.DataAnnotations;

namespace Produtos.Models.Requests
{
    public class AtualizarEstoqueRequest
    {
        [Required(ErrorMessage = "Novo estoque é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "Estoque não pode ser negativo")]
        public int NovoEstoque { get; set; }
    }
}