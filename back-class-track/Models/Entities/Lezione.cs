using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class Lezione
    {
        [Key]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        [ForeignKey("Classe")]
        public int ClasseId { get; set; }

        [ForeignKey("Docente")]
        public int DocenteId { get; set; }

        public string Argomenti { get; set; }
        public string Note { get; set; }

        public Classe Classe { get; set; }
        public Utente Docente { get; set; }

        public ICollection<Presenza> Presenze { get; set; }
    }
}
