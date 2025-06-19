using back_class_track.DTO.Presenze;

namespace back_class_track.DTO.Lezioni
{
    public class LezioneDTO
    {
        public int id { get; set; }
        public DateTime data { get; set; }
        public string argomenti { get; set; }
        public string note { get; set; }
        public int docenteId { get; set; }
        public int classeId { get; set; }

        public int tutorId { get; set; }

        public bool presente { get; set; }
    }
}
