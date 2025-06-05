using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class ClasseCorso
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Classe")]
        public int ClasseId { get; set; }

        [ForeignKey("Corso")]
        public int CorsoId { get; set; }

        public Classe Classe { get; set; }
        public Corso Corso { get; set; }
    }
}
