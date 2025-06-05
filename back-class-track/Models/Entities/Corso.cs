using System.ComponentModel.DataAnnotations;

namespace back_class_track.Models.Entities
{
    public class Corso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string AreaTematica { get; set; }

        public int DurataOre { get; set; }

        public ICollection<ClasseCorso> ClasseCorsi { get; set; }

    }
}
