using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParticipacionDigital.Core.Entities
{
    public class Amonestacion
    {
        public int Id { get; set; }
        
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        
        public int AdminId { get; set; }
        public Usuario? Admin { get; set; }
        
        [Required]
        public string Razon { get; set; } = string.Empty;
        
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public DateTime FechaFin { get; set; }
        
        public bool Activa => DateTime.UtcNow < FechaFin && FechaLevantamiento == null;
        
        public string? RazonLevantamiento { get; set; }
        public DateTime? FechaLevantamiento { get; set; }
        public int? AdminLevantamientoId { get; set; }
        public Usuario? AdminLevantamiento { get; set; }
    }
}
