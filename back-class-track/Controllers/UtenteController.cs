using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_class_track.Data;
using back_class_track.Models.Entities;
using back_class_track.DTO.Utenti;


namespace back_class_track.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtentiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UtentiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/utenti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utente>>> GetUtenti()
        {
            
            var utenti = await _context.Utenti
                .Where(u=> u.ruolo.ToLower() == "studente")
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
    }
}
