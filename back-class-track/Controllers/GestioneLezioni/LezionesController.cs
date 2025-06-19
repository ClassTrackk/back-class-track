using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_class_track.Data;
using back_class_track.Models.Entities;
using back_class_track.DTO.Lezioni;

namespace back_class_track.Controllers.GestioneLezioni
{
    [ApiController]
    [Route("api/lezioni")]
    public class LezioniApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LezioniApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/lezioni
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LezioneDTO>>> GetAll([FromQuery] int? classeId, [FromQuery] int? docenteId, [FromQuery] DateTime? data)
        {
            var query = _context.Lezioni
                .Include(l => l.classe)
                .Include(l => l.docente)
                .AsQueryable();

            if (classeId.HasValue)
                query = query.Where(l => l.classeId == classeId.Value);

            if (docenteId.HasValue)
                query = query.Where(l => l.docenteId == docenteId.Value);

            if (data.HasValue)
                query = query.Where(l => l.data.Date == data.Value.Date);

            var lezioni = await query
                .Select(l => new
                {
                    l.id,
                    l.data,
                    l.argomenti,
                    l.note,
                    classe = new { l.classe.id, l.classe.nome, l.classe.studenti },
                    //docente = new { l.docente.id, l.docente.nome, l.docente.cognome }
                })
                .ToListAsync();

            return Ok(lezioni);
        }

        // GET: api/lezioni/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lezione = await _context.Lezioni.FindAsync(id);
            if (lezione == null)
                return NotFound();

            var lezioneDTO = new LezioneDTO
            {
                id = lezione.id,
                data = lezione.data,
                argomenti = lezione.argomenti,
                note = lezione.note,
                docenteId = lezione.docenteId,
                classeId = lezione.classeId
            };

            return Ok(lezioneDTO);
        }


        // POST: api/lezioni
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLezioneDTO dto) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Imposto la data come UTC per evitare errori
            var dataUtc = DateTime.SpecifyKind(dto.data, DateTimeKind.Utc);

            var lezione = new Lezione
            {
                
                data = dataUtc,
                argomenti = dto.argomenti,
                note = dto.note,
                docenteId = dto.docenteId,
                classeId = dto.classeId
            };

            try
            {
                _context.Lezioni.Add(lezione);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Errore durante la creazione delle lezioni: " + ex.InnerException?.Message);
            }

            var lezioneDTO = new LezioneDTO
            {
                id = lezione.id,             
                data = dataUtc,
                argomenti = lezione.argomenti,
                note = lezione.note,
                docenteId = lezione.docenteId,
                classeId = lezione.classeId
            };

            return CreatedAtAction(nameof(GetById), new { lezioneDTO.id }, lezioneDTO);
        }



        // PUT: api/lezioni/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LezioneDTO dto)
        {
            var lezione = await _context.Lezioni.FindAsync(id);
            if (lezione == null)
                return NotFound();

            lezione.data = dto.data;
            lezione.argomenti = dto.argomenti;
            lezione.note = dto.note;
            lezione.docenteId = dto.docenteId;
            lezione.classeId = dto.classeId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/lezioni/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lezione = await _context.Lezioni.FindAsync(id);
            if (lezione == null)
                return NotFound();

            _context.Lezioni.Remove(lezione);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
