using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_class_track.Models.Entities
{
    public class Voto
    {
        public int id { get; set; }
        public float valutazione { get; set; }
        public string descrizione { get; set; }
        public DateTime dataVerifica { get; set; }

        public int studenteId { get; set; }
        public Utente studente { get; set; }

        public int corsoId { get; set; }
        public Corso corso { get; set; }

    }

}
