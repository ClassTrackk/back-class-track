using back_class_track.Data;
using back_class_track.DTO.Auth;
using back_class_track.DTO.Utenti;
using back_class_track.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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

        #region ENDPOINTS AMMINISTRATORE

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



        [HttpPost("/api/auth/register")]
        public async Task<ActionResult<UserDTO>> PostUtente([FromBody] CreateUtenteDTO dto)
        {
            //VALIDAZIONE INPUT
            if (dto == null || string.IsNullOrEmpty(dto.email) || string.IsNullOrEmpty(dto.password))
            {
                return BadRequest("Dati non validi.");
            }

            //CONTROLLO UNICITA' EMAIL
            if (await _context.Utenti.AnyAsync(u => u.email == dto.email))
            {
                return Conflict("L'email è già registrata.");
            }

            //CREO NUOVO UTENTE
            var nuovoUtente = new Utente
            {
                nome = dto.nome,
                password = BCrypt.Net.BCrypt.HashPassword(dto.password), //HASH DELLA PASSWORD
                cognome = dto.cognome,
                email = dto.email,
                ruolo = dto.ruolo
            };

            try
            {
                _context.Utenti.Add(nuovoUtente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Errore durante il salvataggio: " + ex.InnerException?.Message);
            }

            // Mapping al DTO
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

        #endregion

        #region ENDPOINTS SOLO PER INSEGNATI E TUTOR

        //RITORNA 
        [HttpGet("classe/{classeId}/studenti")]
        public async Task<ActionResult<List<UserDTO>>> GetStudentiClasse(int classeId)
        {
            bool classeEsiste = await _context.Classi.AnyAsync(c => c.id == classeId);
            if (!classeEsiste)
            {
                return NotFound("Classe non trovata.");
            }

            var studenti = await _context.Iscrizioni
                .AsNoTracking()
                .Where(i => i.classeId == classeId)
                .Include(i => i.studente)
                .Select(i => new UserDTO
                {
                    id = i.classeId,
                    nome = i.studente.nome,
                    cognome = i.studente.cognome,
                    email = i.studente.email,
                    ruolo = i.studente.ruolo,
                }).ToListAsync();

            return Ok(studenti);
        }

        #endregion
    }

    #endregion
}

