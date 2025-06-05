using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class DocenteClasse
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Docente")]
        public int DocenteId { get; set; }
        public Utente Docente { get; set; }


        [ForeignKey("Classe")]
        public int ClasseId { get; set; }
        public Classe Classe { get; set; }
    }
}
