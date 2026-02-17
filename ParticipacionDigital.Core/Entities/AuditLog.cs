using System;

namespace ParticipacionDigital.Core.Entities
{
    public class AuditLog : BaseEntity
    {
        public string? UsuarioId { get; set; } // Nullable, as some actions might be anonymous (e.g. failed login)
        public string Action { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty; // Encrypted
        public string UserAgent { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
