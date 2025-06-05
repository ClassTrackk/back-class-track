using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class Lezione
    {
        [Key]
        public int id { get; set; }
        public DateTime data { get; set; }


        [ForeignKey("Classe")]
        public int classeId { get; set; }
        public Classe classe { get; set; }


        [ForeignKey("Docente")]
        public int docenteId { get; set; }
        public Utente docente { get; set; }


        public string argomenti { get; set; }
        public string note { get; set; }


        public ICollection<Presenza> Presenze { get; set; }
    }
}
