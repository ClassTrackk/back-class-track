namespace back_class_track.DTO.Presenze
{
    public class CreatePresenzaDTO
    {
        public int lezioneId { get; set; }
        public List<PresenzaStudenteDTO> presenze { get; set; }
    }
}
