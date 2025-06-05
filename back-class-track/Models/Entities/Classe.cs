using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class Classe
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string nome { get; set; }

        public int annoFormativo { get; set; }

        [ForeignKey("Tutor")]
        public int tutorId { get; set; }
        public Utente tutor { get; set; }

        public ICollection<Iscrizione> iscrizioni { get; set; }
        public ICollection<ClasseCorso> classiCorsi { get; set; }
        public ICollection<DocenteClasse> docentiClassi { get; set; }
        public ICollection<Lezione> lezioni { get; set; }
    }
}
