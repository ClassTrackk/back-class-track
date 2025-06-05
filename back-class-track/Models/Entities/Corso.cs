using System.ComponentModel.DataAnnotations;

namespace back_class_track.Models.Entities
{
    public class Corso
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string nome { get; set; }

        public string categoriaGenerale { get; set; }

        public int durataOre { get; set; }

        public ICollection<ClasseCorso> classeCorsi { get; set; }

    }
}
