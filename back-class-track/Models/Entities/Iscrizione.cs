using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class Iscrizione
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Studente")]
        public int StudenteId { get; set; }
        public Utente Studente { get; set; }


        [ForeignKey("Classe")]
        public int ClasseId { get; set; }
        public Classe Classe { get; set; }
    }
}
