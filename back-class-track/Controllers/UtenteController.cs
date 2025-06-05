using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back_class_track.Data;
using back_class_track.Models.Entities;


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
            var utenti = await _context.Utenti.ToListAsync();
            return Ok(utenti);
        }
    }
}
