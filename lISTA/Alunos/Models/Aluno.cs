using System.ComponentModel.DataAnnotations;

namespace Alunos.Models
{
    public class Aluno
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "Nome deve conter apenas letras e espaços")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "RA é obrigatório")]
        [RaValidation(ErrorMessage = "RA deve começar com 'RA' seguido de 6 dígitos")]
        public string Ra { get; set; }
        
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "CPF é obrigatório")]
        [CpfValidation(ErrorMessage = "CPF deve ser válido")]
        public string Cpf { get; set; }
        
        [Required(ErrorMessage = "Status Ativo é obrigatório")]
        public bool Ativo { get; set; }
    }
}