using Microsoft.AspNetCore.Mvc;
using Alunos.Models;

namespace Alunos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private static List<Aluno> alunos = new List<Aluno>();
        
        [HttpPost]
        public IActionResult CriarAluno([FromBody] Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (alunos.Any(a => a.Ra == aluno.Ra))
            {
                return BadRequest("RA já cadastrado no sistema");
            }
            
            if (alunos.Any(a => a.Email == aluno.Email))
            {
                return BadRequest("Email já cadastrado no sistema");
            }
            
            if (alunos.Any(a => a.Cpf == aluno.Cpf))
            {
                return BadRequest("CPF já cadastrado no sistema");
            }
            
            alunos.Add(aluno);
            return Ok(new { message = "Aluno criado com sucesso", aluno });
        }
        
        [HttpGet]
        public IActionResult ObterAlunos()
        {
            return Ok(alunos);
        }
        
        [HttpGet("{ra}")]
        public IActionResult ObterAluno(string ra)
        {
            var aluno = alunos.FirstOrDefault(a => a.Ra == ra);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            return Ok(aluno);
        }
        
        [HttpPut("{ra}")]
        public IActionResult AtualizarAluno(string ra, [FromBody] Aluno alunoAtualizado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var aluno = alunos.FirstOrDefault(a => a.Ra == ra);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            
            if (alunos.Any(a => a.Ra != ra && a.Email == alunoAtualizado.Email))
            {
                return BadRequest("Email já cadastrado para outro aluno");
            }
            
            aluno.Nome = alunoAtualizado.Nome;
            aluno.Email = alunoAtualizado.Email;
            aluno.Cpf = alunoAtualizado.Cpf;
            aluno.Ativo = alunoAtualizado.Ativo;
            
            return Ok(new { message = "Aluno atualizado com sucesso", aluno });
        }
        
        [HttpDelete("{ra}")]
        public IActionResult DeletarAluno(string ra)
        {
            var aluno = alunos.FirstOrDefault(a => a.Ra == ra);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            
            alunos.Remove(aluno);
            return Ok(new { message = "Aluno removido com sucesso" });
        }
    }
}