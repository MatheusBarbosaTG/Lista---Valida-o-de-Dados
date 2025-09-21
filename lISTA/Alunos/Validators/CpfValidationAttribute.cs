using System.ComponentModel.DataAnnotations;

namespace Alunos.Validators
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            
            string cpf = value.ToString().Replace(".", "").Replace("-", "");
            
            if (cpf.Length != 11) return false;
            
            if (cpf.All(c => c == cpf[0])) return false;
            
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            
            int primeiroDigito = soma % 11;
            primeiroDigito = primeiroDigito < 2 ? 0 : 11 - primeiroDigito;
            
            if (int.Parse(cpf[9].ToString()) != primeiroDigito) return false;
            
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            
            int segundoDigito = soma % 11;
            segundoDigito = segundoDigito < 2 ? 0 : 11 - segundoDigito;
            
            return int.Parse(cpf[10].ToString()) == segundoDigito;
        }
        
        public override string FormatErrorMessage(string name)
        {
            return $"{name} deve ser um CPF válido.";
        }
    }
}