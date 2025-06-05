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
        public Mansione Ruolo { get; set; }
    }

    public enum Mansione
    {
        None = 0,
        Studente,
        Docente,
        Tutor,
        Amministratore
    }

}
