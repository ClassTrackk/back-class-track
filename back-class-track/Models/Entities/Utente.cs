using System.ComponentModel.DataAnnotations;


namespace back_class_track.Models.Entities
{
    public class Utente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cognome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Ruolo { get; set; }

        public ICollection<Lezione> LezioniComeDocente { get; set; }
        public ICollection<Presenza> PresenzeComeStudente { get; set; }
        public ICollection<Iscrizione> Iscrizioni { get; set; }

    }
}
