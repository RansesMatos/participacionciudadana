using System;

namespace ParticipacionDigital.Core.Entities
{
    public class RespuestaInquietud : BaseEntity
    {
        public string Texto { get; set; } = string.Empty;
        
        public int InquietudId { get; set; }
        public Inquietud Inquietud { get; set; } = null!;

        public int AutorId { get; set; }
        public Usuario Autor { get; set; } = null!;

        public bool EsAutoridad { get; set; } // Identifies if the reply is from an Admin/Moderator
        public bool Reportado { get; set; } = false;
        public bool Activa { get; set; } = true;
    }
}
