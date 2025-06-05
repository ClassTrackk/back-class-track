using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class DocenteClasse
    {
        [Key]
        public int id { get; set; }


        [ForeignKey("Docente")]
        public int docenteId { get; set; }
        public Utente docente { get; set; }


        [ForeignKey("Classe")]
        public int classeId { get; set; }
        public Classe classe { get; set; }
    }
}
