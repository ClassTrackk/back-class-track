using back_class_track.DTO.Lezioni;

namespace back_class_track.DTO.Utenti
{
    public class UserDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string email { get; set; }
        public string ruolo { get; set; }
        public List<LezioneDTO> lezioniComeStudente { get; set; }

    }
}
