using System.Collections.Generic;

namespace ParticipacionDigital.Core.Entities
{
    public class Alcaldia : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty; // Norte, Sur, Este, Gran Santo Domingo

        // Navigation property
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public ICollection<Encuesta> Encuestas { get; set; } = new List<Encuesta>();
    }
}
