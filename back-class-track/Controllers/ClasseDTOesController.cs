using back_class_track.Data;
using back_class_track.DTO.Classi;
using back_class_track.DTO.Lezioni;
using back_class_track.DTO.Presenze;
using back_class_track.DTO.Utenti;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_class_track.Controllers
{
    [Route("api/classi")]
    [ApiController]
    public class ClasseDTOesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClasseDTOesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ClasseDTOes
        [HttpGet]

        [HttpGet("classi/{classeId}/lezioni")]
        public async Task<ActionResult<List<LezioneConPresenzeDTO>>> GetLezioniConPresenzePerClasse(int classeId)
        {
            var lezioni = await _context.Lezioni
                .Where(l => l.classeId == classeId) 
                .Include(l => l.Presenze)
                    .ThenInclude(p => p.studente) 
                .OrderBy(l => l.data)
                .Select(l => new LezioneConPresenzeDTO
                {
                    id = l.id,
                    data = l.data,
                    argomenti = l.argomenti,
                    note = l.note,
                    docenteId = l.docenteId,
                    tutorId = l.tutorId,
                    presenze = l.Presenze.Select(p => new PresenzaDTO
                    {
                        id = p.id,
                        studenteId = p.studenteId,
                        presente = p.presente
                        // puoi aggiungere nome studente se hai i dati
                    }).ToList()
                })
                .ToListAsync();

            if (lezioni == null || lezioni.Count == 0)
                return NotFound("Nessuna lezione trovata per questa classe.");

            return Ok(lezioni);
        }


        // GET: api/ClasseDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClasseDTO>> GetClasseDTO(int id)
        {
            var classe = await _context.Classi
                .Where(c => c.id == id)
                .Include(c => c.studenti)
                .Include(c => c.lezioni)
                .Select(c => new ClasseDTO
                {
                    id = c.id,
                    nome = c.nome,

                    studenti = c.iscrizioni.Select(uc => new UserDTO
                    {
                        id = uc.id,
                        nome = uc.studente.nome,
                        cognome = uc.studente.cognome,
                        email = uc.studente.email
                    }).ToList(),

                   
                }).FirstOrDefaultAsync();
            if(classe == null)
            {
                return NotFound(new {message = "Classe non trovata" });
            }

           return Ok(classe);
        }

    }
}
