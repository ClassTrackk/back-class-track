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
        
        //chiave esterna che fa riferimento alla tabella Utenti
        [ForeignKey("Tutor")]
        public int TutorId { get; set; }

        public Utente Tutor { get; set; }


    }
}
