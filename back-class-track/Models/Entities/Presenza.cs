using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class Presenza
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Lezione")]
        public int LezioneId{ get; set; }
        public Lezione Lezione { get; set; }


        [ForeignKey("Studente")]
        public int StudenteId { get; set; }
        public Utente Studente { get; set; }

        public bool Presente { get; set; }
    }
}
