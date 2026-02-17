using System;
using System.Collections.Generic;

namespace ParticipacionDigital.Core.Entities
{
    public class Inquietud : BaseEntity
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Reportado { get; set; } = false;
        public bool Activa { get; set; } = true;

        public int AutorId { get; set; }
        public Usuario Autor { get; set; } = null!;

        public ICollection<RespuestaInquietud> Respuestas { get; set; } = new List<RespuestaInquietud>();
    }
}
