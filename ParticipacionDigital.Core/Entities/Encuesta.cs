using System;
using System.Collections.Generic;

namespace ParticipacionDigital.Core.Entities
{
    public class Encuesta : BaseEntity
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activa { get; set; } = true;
        
        public int CreadorId { get; set; }
        public Usuario Creador { get; set; } = null!;

        public int? AlcaldiaId { get; set; }
        public Alcaldia? Alcaldia { get; set; }

        public ICollection<Opcion> Opciones { get; set; } = new List<Opcion>();
        public ICollection<Voto> Votos { get; set; } = new List<Voto>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
