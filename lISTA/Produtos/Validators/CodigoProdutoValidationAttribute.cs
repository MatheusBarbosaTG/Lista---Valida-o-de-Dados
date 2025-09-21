using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Produtos.Validators
{
    public class CodigoProdutoValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            
            string codigo = value.ToString();
            
            if (codigo.Length != 8) return false;
            
            string pattern = @"^[A-Z]{3}-\d{4}$";
            return Regex.IsMatch(codigo, pattern);
        }
        
        public override string FormatErrorMessage(string name)
        {
            return $"{name} deve seguir o formato 'AAA-1234' (3 letras maiúsculas, hífen e 4 números).";
        }
    }
}