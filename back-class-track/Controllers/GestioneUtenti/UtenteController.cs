using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_class_track.Data;
using back_class_track.DTO.Utenti;
using back_class_track.Models.Entities;
using back_class_track.DTO.Auth;


namespace back_class_track.Controllers.GestioneUtenti
{
    [ApiController]
    [Route("api/users")]
    public class UtentiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UtentiController(AppDbContext context)
        {
            _context = context;
        }

        #region CONTROLLERS PER AMMINISTRATORE
        //ROUTE PER AVERE TUTTI GLI UTENTI NEL DB
        // GET: api/users
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUtenti()
        {
            var utenti = await _context.Utenti
                .Select(u => new UserDTO
            {
                id = u.id,
                nome = u.nome,
                cognome = u.cognome,
                email = u.email,
                ruolo = u.ruolo,
            }).ToListAsync();

            return Ok(utenti);
        }

        //ROUTE PER AVERE UN SINGOLO UTENTE
        //GET: api/users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUtente(int id)
        {
            var utente = await _context.Utenti
                .Where(u => u.id == id)
                .Select(u => new UserDTO
                {
                    id = u.id,
                    nome = u.nome,
                    cognome = u.cognome,
                    email = u.email,
                    ruolo = u.ruolo,
                })
                .FirstOrDefaultAsync();

            if (utente == null)
                return NotFound(new { message = "Utente non trovato" });

            return Ok(utente);
        }


        //CREO NUOVO UTENTE
        public async Task<ActionResult<UserDTO>> PostUtente([FromBody] CreateUtenteDTO dto)
        {
            var nuovoUtente = new Utente
            {
               nome = dto.nome,
               password = dto.password,
               cognome = dto.cognome,
               email = dto.email,
               ruolo = dto.ruolo
            };

            _context.Utenti.Add(nuovoUtente);
            await _context.SaveChangesAsync();

            var utenteDTO = new UserDTO
            {
                id = nuovoUtente.id,
                nome = nuovoUtente.nome,
                cognome = nuovoUtente.cognome,
                email = nuovoUtente.email,
                ruolo = nuovoUtente.ruolo
            };

            return CreatedAtAction(nameof(GetUtente), new { id = utenteDTO.id }, utenteDTO);
        }
    }

    #endregion
}

