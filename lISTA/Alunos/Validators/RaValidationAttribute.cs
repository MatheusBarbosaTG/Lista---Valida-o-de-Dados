using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Alunos.Validators
{
    public class RaValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            
            string ra = value.ToString();
            string pattern = @"^RA\d{6}$";
            return Regex.IsMatch(ra, pattern);
        }
        
        public override string FormatErrorMessage(string name)
        {
            return $"{name} deve começar com 'RA' seguido de exatamente 6 dígitos.";
        }
    }
}