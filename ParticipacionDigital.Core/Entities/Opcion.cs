namespace ParticipacionDigital.Core.Entities
{
    public class Opcion : BaseEntity
    {
        public string Texto { get; set; } = string.Empty;
        
        public int EncuestaId { get; set; }
        public Encuesta Encuesta { get; set; } = null!;
    }
}
