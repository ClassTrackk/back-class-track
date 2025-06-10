using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class Voto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public float valutazione { get; set; }

        [Required]
        public DateTime dataVerifica { get; set; }
        
        public string descrizione { get; set; }

        [ForeignKey("Studente")]
        public int studenteId { get; set; }
        public Utente studente { get; set; }

        [ForeignKey("Corso")]
        public int corsoId { get; set; }
        public Corso corso { get; set; }

    }
}
