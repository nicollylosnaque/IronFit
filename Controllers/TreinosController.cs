using IronFit.Data;
using IronFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IronFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public TreinosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Treino>> CreateTreino(CriarTreinoDto treinoDto)
        {
            var treino = new Treino
            {
                Data = treinoDto.Data,
                NivelDificuldade = treinoDto.NivelDificuldade,
                TipoTreino = treinoDto.TipoTreino,
                AlunoId = treinoDto.AlunoId,
                PersonalId = treinoDto.PersonalId
            };

            _context.Treinos.Add(treino);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateTreino), new { id = treino.Id }, treino);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CriarTreinoDto>>> GetTreino()
        {
            var treinos = await _context.Treinos
                .Select(t => new InfoTreinosDto
                {
                    Id = t.Id,
                    Data = t.Data,
                    TipoTreino = t.TipoTreino,
                    NivelDificuldade = t.NivelDificuldade,
                    AlunoId = t.AlunoId,
                    PersonalId = t.PersonalId
                })
                .ToListAsync();

            return Ok(treinos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InfoTreinosDto>> GetTreino(int id)
        {
            var treino = await _context.Treinos
                .Select(t => new InfoTreinosDto
                {
                    Id = t.Id,
                    Data = t.Data,
                    TipoTreino = t.TipoTreino,
                    NivelDificuldade = t.NivelDificuldade,
                    AlunoId = t.AlunoId,
                    PersonalId = t.PersonalId
                })
                .FirstOrDefaultAsync(t => t.Id == id);
            if (treino == null)
            {
                return NotFound();
            }
            return Ok(treino);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTreino(int id, CriarTreinoDto treinoDto)
        {
            var treino = await _context.Treinos.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }
            treino.Data = treinoDto.Data;
            treino.NivelDificuldade = treinoDto.NivelDificuldade;
            treino.TipoTreino = treinoDto.TipoTreino;
            treino.AlunoId = treinoDto.AlunoId;
            treino.PersonalId = treinoDto.PersonalId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreino(int id)
        {
            var treino = await _context.Treinos.FindAsync(id);
            if (treino  == null)
            {
                return NotFound();
            }
            _context.Treinos.Remove(treino);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
        




      

