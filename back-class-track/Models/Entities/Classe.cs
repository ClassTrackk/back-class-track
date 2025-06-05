using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class Classe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public int AnnoFormativo { get; set; }

        [ForeignKey("Tutor")]
        public int TutorId { get; set; }
        public Utente Tutor { get; set; }

        public ICollection<Iscrizione> Iscrizioni { get; set; }
        public ICollection<ClasseCorso> ClassiCorsi { get; set; }
        public ICollection<DocenteClasse> DocentiClassi { get; set; }
        public ICollection<Lezione> Lezioni { get; set; }
    }
}
