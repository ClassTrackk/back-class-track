namespace back_class_track.Models.Entities
{
    public class Utente
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }

        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
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
