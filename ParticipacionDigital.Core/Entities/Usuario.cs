using Microsoft.AspNetCore.Identity;
using ParticipacionDigital.Core.Enums;
using System.Collections.Generic;

namespace ParticipacionDigital.Core.Entities
{
    public class Usuario : IdentityUser<int>
    {
        public string Nombre { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; } = RolUsuario.Ciudadano;
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public int Admoniciones { get; set; } = 0;
        
        public string Cedula { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public int? AlcaldiaId { get; set; }
        public Alcaldia? Alcaldia { get; set; }
        public bool MustChangePassword { get; set; } = false;

        // Relaciones
        public ICollection<Voto> Votos { get; set; } = new List<Voto>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ICollection<Encuesta> EncuestasCreadas { get; set; } = new List<Encuesta>();
        public ICollection<Amonestacion> Amonestaciones { get; set; } = new List<Amonestacion>();
    }
}
