using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_class_track.Data;
using back_class_track.Models.Entities;
using back_class_track.DTO.Voti;

namespace back_class_track.Controllers.GestioneVoti
{
    [ApiController]
    [Route("api/voti")]
    public class VotiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VotiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/voti
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var voti = await _context.Voti
                .Include(v => v.corso)
                .Include(v => v.studente)
                .Select(v => new
                {
                    v.id,
                    v.valutazione,
                    v.dataVerifica,
                    v.descrizione,
                    corso = new { v.corso.id, v.corso.nome },
                    studente = new { v.studente.id, v.studente.nome, v.studente.cognome }
                })
                .ToListAsync();

            return Ok(voti);
        }

        // GET: api/voti/5 => Singolo voto
        [HttpGet("{votoId}")]
        public async Task<IActionResult> GetVotoById(int votoId)
        {
            var voto = await _context.Voti
                .Include(v => v.corso)
                .Include(v => v.studente)
                .Where(v => v.id == votoId)
                .Select(v => new
                {
                    v.id,
                    v.valutazione,
                    v.dataVerifica,
                    v.descrizione,
                    corso = new { v.corso.id, v.corso.nome },
                    studente = new { v.studente.id, v.studente.nome, v.studente.cognome }
                })
                .FirstOrDefaultAsync();

            if (voto == null)
                return NotFound();

            return Ok(voto);
        }

        // GET: api/voti/studente/3 => Tutti i voti per uno studente
        [HttpGet("studente/{studenteId}")]
        public async Task<IActionResult> GetByStudenteId(int studenteId)
        {
            var voti = await _context.Voti
                .Where(v => v.studenteId == studenteId)
                .Include(v => v.corso)
                .Select(v => new
                {
                    v.id,
                    v.studente.nome,
                    v.valutazione,
                    v.dataVerifica,
                    v.descrizione,
                    corso = new { v.corso.id, v.corso.nome }
                })
                .ToListAsync();

            if (!voti.Any())
                return NotFound(new { message = "Nessun voto trovato per questo studente." });

            return Ok(voti);
        }

        // POST: api/voti
        [HttpPost]
        public async Task<ActionResult<VotiDTO>> Create([FromBody] VotiDTO votoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var voto = new Voto
            {
                valutazione = votoDTO.valutazione,
                dataVerifica = DateTime.SpecifyKind(votoDTO.dataVerifica, DateTimeKind.Utc),
                descrizione = votoDTO.descrizione,
                studenteId = votoDTO.studenteId,
                corsoId = votoDTO.corsoId
            };

            _context.Voti.Add(voto);
            await _context.SaveChangesAsync();

            // Popola id ritornato dal database
            votoDTO.id = voto.id;

            return CreatedAtAction(nameof(GetVotoById), new { votoId = voto.id }, votoDTO);
        }



        // PUT: api/voti/5
        [HttpPut("{votoId}")]
        public async Task<IActionResult> Update(int votoId, [FromBody] Voto voto)
        {
            if (votoId != voto.id)
                return BadRequest(new { message = "ID nel percorso non corrisponde all'ID del corpo richiesta." });

            _context.Entry(voto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Voti.Any(v => v.id == votoId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/voti/5
        [HttpDelete("{votoId}")]
        public async Task<IActionResult> Delete(int votoId)
        {
            var voto = await _context.Voti.FindAsync(votoId);
            if (voto == null)
                return NotFound();

            _context.Voti.Remove(voto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
