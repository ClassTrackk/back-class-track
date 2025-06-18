using back_class_track.Data;
using back_class_track.DTO.Lezioni;
using back_class_track.DTO.Presenze;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_class_track.Controllers
{
    [Route("api")]
    [ApiController]
    public class LezioneDTOesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LezioneDTOesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: classi/{classeId}/registro
        [HttpGet("classi/{classeId}/registro-lezioni")]
        public async Task<ActionResult<List<LezioneConPresenzeDTO>>> GetRegistroLezioni(int classeId)
        {
            var lezioni = await _context.Lezioni
                .Where(l => l.classeId == classeId)
                .Include(l => l.docente)
                .Include(l => l.Presenze)
                    .ThenInclude(p => p.studente)
                .OrderBy(l => l.data)
                .ToListAsync();
            Console.WriteLine($"Lezioni trovate: {lezioni.Count}");
            if (!lezioni.Any())
                return NotFound($"Nessuna lezione trovata per la classe {classeId}");

            var result = lezioni.Select(l => new LezioneConPresenzeDTO
            {
                id = l.id,
                data = l.data,
                argomenti = l.argomenti,
                note = l.note,
                docenteId = l.docenteId,
                docenteNome = l.docente.nome + " " + l.docente.cognome,
                tutorId = l.tutorId,
                presenze = l.Presenze.Select(p => new PresenzaDTO
                {
                    id = p.id,
                    studenteId = p.studenteId,
                    nomeStudente = p.studente.nome + " " + p.studente.cognome,
                    presente = p.presente
                }).ToList()
            }).ToList();

            return Ok(result);
        }


    }
}
