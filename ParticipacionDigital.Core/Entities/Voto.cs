using System;

namespace ParticipacionDigital.Core.Entities
{
    public class Voto : BaseEntity
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int EncuestaId { get; set; }
        public Encuesta Encuesta { get; set; } = null!;

        public int OpcionId { get; set; } // The option selected
        public Opcion Opcion { get; set; } = null!;

        public DateTime FechaVoto { get; set; } = DateTime.UtcNow;


    }
}
