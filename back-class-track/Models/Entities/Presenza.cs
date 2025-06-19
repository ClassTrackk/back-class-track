using back_class_track.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace back_class_track.Models.Entities
{
    [Table("Presenza")]
    public class Presenza
    {
        [Key]
        public int id { get; set; }


        [ForeignKey("Lezione")]
        public int lezioneId{ get; set; }
        public Lezione lezione { get; set; }


        [ForeignKey("Studente")]
        public int studenteId { get; set; }
        public Utente studente { get; set; }

        [ForeignKey("Tutor")]
        public int tutorId { get; set; }
        public Utente tutor {get; set; }
        public bool presente { get; set; }
    }
}
