using System.ComponentModel.DataAnnotations;

namespace Produtos.Models
{
    public class Produto
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Descrição deve ter entre 3 e 200 caracteres")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "Preço é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }
        
        [Required(ErrorMessage = "Estoque é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "Estoque não pode ser negativo")]
        public int Estoque { get; set; }
        
        [Required(ErrorMessage = "Código do produto é obrigatório")]
        [CodigoProdutoValidation(ErrorMessage = "Código do produto deve seguir o formato 'AAA-1234'")]
        public string CodigoProduto { get; set; }
    }
}