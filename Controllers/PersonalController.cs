using IronFit.Data;
using IronFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SimuladoOficina.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonalController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personal>>> GetPersonal()
        {
            return await _context.Personals.ToListAsync();
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalDto>> GetPersonal(int id)
        {
            var personal = await _context.Personals.FindAsync(id);

            if (personal     == null)
            {
                return NotFound();
            }

            return new PersonalDto
            {
                Nome = personal.Nome,
                Telefone = personal.Telefone,
             
            };
        }

     
        [HttpPost]
        public async Task<ActionResult<Personal>> CreatePersonal(PersonalDto PersonalDto)
        {
            var personal = new Personal
            {
                Id = PersonalDto.Id,
                CPF = PersonalDto.CPF,
                Nome = PersonalDto.Nome,
                Telefone = PersonalDto.Telefone,
             
            };

            _context.Personals.Add(personal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonal), new { id = personal.Id }, personal);
        }

       
        

      
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonal(int id, PersonalDto personalDto)
        {
            var personal = await _context.Personals.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            personal.Nome = personalDto.Nome;
            personal.Telefone = personalDto.Telefone;
          
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonal(int id)
        {
            var personal = await _context.Personals.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            _context.Personals.Remove(personal);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
