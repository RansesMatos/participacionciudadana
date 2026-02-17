using System;

namespace ParticipacionDigital.Core.Entities
{
    public class Actividad : BaseEntity
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Lugar { get; set; } = string.Empty;
        public DateTime FechaRealizacion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string? ImagenPath { get; set; }
        public bool IsApproved { get; set; }

        public int? AlcaldiaId { get; set; }
        public Alcaldia? Alcaldia { get; set; }
    }
}
