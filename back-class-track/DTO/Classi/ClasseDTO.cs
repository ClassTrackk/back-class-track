using back_class_track.DTO.Corsi;
using back_class_track.DTO.Utenti;

namespace back_class_track.DTO.Classi
{
    public class ClasseDTO
    {
        public int id {  get; set; }
        public string nome { get; set; }
        public List<UserDTO> studenti { get; set; }

        public List<CorsoDTO> corsi { get; set; }

        public UserDTO docente { get; set; }
        public UserDTO tutor { get; set; }
    }
}
