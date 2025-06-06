namespace back_class_track.DTO.Auth
{
    public class CreateUtenteDTO
    {
        public string nome { get; set; }
        public string cognome { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string ruolo { get; set; }
    }
}
