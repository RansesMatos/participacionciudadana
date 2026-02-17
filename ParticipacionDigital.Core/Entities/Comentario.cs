using System;

namespace ParticipacionDigital.Core.Entities
{
    public class Comentario : BaseEntity
    {
        public string Texto { get; set; } = string.Empty;
        
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int EncuestaId { get; set; }
        public Encuesta Encuesta { get; set; } = null!;
    }
}
