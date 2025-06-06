namespace back_class_track.DTO.Presenze
{
    public class PresenzaDTO
    {
        public int id { get; set; }
        public int lezioneId { get; set; }
        public int studenteId { get; set; }
        public bool presente { get; set; }
    }
}
