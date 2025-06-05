using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class ClasseCorso
    {
        [Key]
        public int id { get; set; }
        
        [ForeignKey("Classe")]
        public int classeId { get; set; }

        [ForeignKey("Corso")]
        public int corsoId { get; set; }

        public Classe classe { get; set; }
        public Corso corso { get; set; }
    }
}
