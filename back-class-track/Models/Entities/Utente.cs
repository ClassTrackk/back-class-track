using System.ComponentModel.DataAnnotations;


namespace back_class_track.Models.Entities
{
    public class Utente
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string cognome { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string ruolo { get; set; }

        public ICollection<Lezione> lezioniComeDocente { get; set; }
        public ICollection<Presenza> PresenzeComeStudente { get; set; }
        public ICollection<Iscrizione> Iscrizioni { get; set; }

    }
}
