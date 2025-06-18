using back_class_track.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Iscrizioni")]

public class Iscrizione
{
    [Key]
    public int id { get; set; }

    [ForeignKey("Studente")]
    public int studenteId { get; set; }
    public Utente studente { get; set; }

    [ForeignKey("Classe")]
    public int classeId { get; set; }
    public Classe classe { get; set; }
}
