namespace back_class_track.DTO.Voti
{
    public class VotiDTO
    {
        public int id { get; set; }
        public float valutazione { get; set; }

        public DateTime dataVerifica { get; set; }

        public string descrizione { get; set; }

        public int studenteId { get; set; }
        public int corsoId { get; set; }
    }
}
