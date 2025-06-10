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

        public List<Lezione> lezioniComeDocente { get; set; }
        public List<Presenza> PresenzeComeStudente { get; set; }
        public List<Iscrizione> Iscrizioni { get; set; }
        public List<Voto> Voti {  get; set; }

    }
}
