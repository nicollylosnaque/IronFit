using IronFit.Data;
using IronFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IronFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunoController(AppDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAlunos()
        {
            return await _context.Alunos.Select(a => new AlunoDto
            {
                Id = a.Id,
                Nome = a.Nome,
                Telefone = a.Telefone,
                CPF = a.CPF,
                PersonalId = a.PersonalId   
            }).ToListAsync();
        }

 
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDto>> GetAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return new AlunoDto
            {
                
                Nome = aluno.Nome,
                Telefone = aluno.Telefone
              
            };

        }

       
        [HttpPost]
        public async Task<ActionResult<AlunoDto>> CreateAluno(AlunoDto alunoDto)
        {
            var aluno = new Aluno   
            {
                Nome = alunoDto.Nome,
                Telefone = alunoDto.Telefone,
                CPF = alunoDto.CPF,
                PersonalId = alunoDto.PersonalId
            };

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, alunoDto);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAluno(int id, AlunoDto alunoDto)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            aluno.Nome = alunoDto.Nome;
            aluno.Telefone = alunoDto.Telefone;
            aluno.CPF = alunoDto.CPF;
            aluno.PersonalId = alunoDto.PersonalId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}