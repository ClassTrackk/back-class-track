using back_class_track.Data;
using back_class_track.DTO.Auth;
using back_class_track.DTO.Classi;
using back_class_track.DTO.Corsi;
using back_class_track.DTO.Lezioni;
using back_class_track.DTO.Utenti;
using back_class_track.Models.Entities;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;


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
        
        [HttpPost("/api/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _context.Utenti.FirstOrDefaultAsync(u => u.email == loginDTO.email);
            if (user == null)
            {
                return NotFound();
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTO.password, user.password);
            if (!isPasswordValid)
            {
                return Unauthorized();
            }
            return Ok(
                new
                {
                    id = user.id,
                    nome = user.nome,
                    cognome = user.cognome,
                    email = user.email,
                    ruolo = user.ruolo,
                }
                );

        }

        //ROUTE PER AVERE TUTTI GLI UTENTI NEL DB
        // GET: api/users
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

        //Login 
        [HttpPost()]
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


        //Registro dello studente 
        [HttpGet("{id}/registro")]
        public async Task<ActionResult<UserDTO>>GetUtentePerRegistro(int id)
        {
            var utente = await _context.Utenti
                .Where(u => u.id == id && u.ruolo == "Studente").
                Select(u => new UserDTO
                {
                    id = u.id,
                    nome = u.nome,
                    cognome = u.cognome,
                    email = u.email,
                    ruolo = u.ruolo, 
                   lezioniComeStudente = u.PresenzeComeStudente.Select(
                       p => new LezioneDTO
                       {
                           id = p.lezione.id,
                           data = p.lezione.data,
                           argomenti = p.lezione.argomenti,
                           note = p.lezione.note,
                           docenteId = p.lezione.docenteId,
                           classeId = p.lezione.classeId,
                           presente = p.presente
                       }).ToList(),
                   
                }).FirstOrDefaultAsync();
            if (utente == null) { return NotFound(new {message = " Utente non trovato nel registro" }); }


            return Ok(utente);

            
        }


        //PUT api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Utente dto)
        {
            if (dto == null || id != dto.id)
            {
                return BadRequest("Dati non validi");
            }
            var utente = await _context.Utenti.FindAsync(id);
            if (utente == null)
            {
                return BadRequest("Utente non trovato");
            }
            //Aggiorno i campi
            utente.nome = dto.nome;
            utente.cognome = dto.cognome;
            utente.email = dto.email;
            utente.ruolo = dto.ruolo;

            if (!string.IsNullOrEmpty(dto.email) && !string.IsNullOrEmpty(dto.password))
            {
                utente.password = BCrypt.Net.BCrypt.HashPassword(dto.password);
            }
            try
            {
                _context.Utenti.Update(utente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        //DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            Utente? utente =  await _context.Utenti.FindAsync(id);

            if(utente == null)
            {
                return BadRequest("Utente non trovato");
            }
            try
            {
                _context.Utenti.Remove(utente);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch(DbUpdateException ex)
            {
                return BadRequest(ex);
            }
        }
        //GET api/users/insegnante/{id}
        [HttpGet("insegnante/{id}")]
        public async Task<ActionResult<UserDTO>> getInsegnanteConCorsi(int id)
        {
            //Otteniamo l'insegnante
            var docente = await _context.Utenti.Where(u => u.id == id && u.ruolo == "Docente").Select(u => new UserDTO
            {
                id = u.id,
                nome = u.nome,
                cognome = u.cognome,
                email = u.email,
                ruolo = u.ruolo,
            }).FirstOrDefaultAsync();
            
            if (docente == null) return NotFound(new { message = "Docente non trovato" });

            var classiConCorsi = await _context.DocentiClassi.Where(dc => dc.docenteId == id)
                .Select(dc => new ClasseDTO
                {
                    id = dc.classe.id,
                    nome = dc.classe.nome,

                    corsi = _context.ClassiCorsi.Where(cc => cc.classeId == dc.classeId).Select(cc => new CorsoDTO
                    {
                        id = cc.id,
                        nome = cc.corso.nome,
                        categoriaGenerale = cc.corso.categoriaGenerale,
                        durataOre = cc.corso.durataOre,
                    }).ToList()
                }).ToListAsync();
            return Ok(new
            {
                docente,
                classiConCorsi,
            });
                }


        //Register
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
                    id = i.studenteId,
                    nome = i.studente.nome,
                    cognome = i.studente.cognome,
                    email = i.studente.email,
                    ruolo = i.studente.ruolo,
                }).ToListAsync();

            return Ok(studenti);
        }

       
        #endregion

        #region ENDPOINTS SOLO PER INSEGNATI E TUTOR


        #endregion
    }

    #endregion
}

