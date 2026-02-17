using Microsoft.EntityFrameworkCore;
using ParticipacionDigital.Infrastructure.Data;
using ParticipacionDigital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParticipacionDigital.Web.Services
{
    public class DashboardService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public DashboardService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<int> GetTotalVotos(int? alcaldiaId, DateTime? start = null, DateTime? end = null)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.Votos.AsQueryable();

            if (alcaldiaId.HasValue)
            {
                // Votos de usuarios de esa alcaldia, o en encuestas de esa alcaldia?
                // Mejor: Votos en encuestas publicadas por esa alcaldia (o usuarios de esa alcaldia si es global)
                // Simplificacion: Votos en encuestas de esa alcaldia.
                 query = query.Where(v => v.Encuesta.AlcaldiaId == alcaldiaId);
            }

            if (start.HasValue) query = query.Where(v => v.FechaVoto >= start.Value);
            if (end.HasValue) 
            {
                var endDate = end.Value.Date.AddDays(1); // End of the day (start of next day)
                query = query.Where(v => v.FechaVoto < endDate);
            }

            return await query.CountAsync();
        }

        public async Task<int> GetTotalEncuestasActivas(int? alcaldiaId)
        {
            using var context = _dbFactory.CreateDbContext();
             var query = context.Encuestas.Where(e => e.Activa && e.FechaFin > DateTime.UtcNow);

             if (alcaldiaId.HasValue)
                query = query.Where(e => e.AlcaldiaId == alcaldiaId);

            return await query.CountAsync();
        }

        public async Task<int> GetOpenInquietudes(int? alcaldiaId)
        {
            using var context = _dbFactory.CreateDbContext();
            // Inquietud doesn't strictly have AlcaldiaId, but Author does.
            // Assuming Inquietud has Author -> Alcaldia
            var query = context.Inquietudes.Include(i => i.Autor).Where(i => !i.Reportado); // Only standard ones

            if (alcaldiaId.HasValue)
                query = query.Where(i => i.Autor.AlcaldiaId == alcaldiaId);
            
            // "Open" definition needed. For now just count total or maybe last 30 days?
            // Let's assume "Pending Reply" if we track that. 
            // For now, just "Total Inquietudes" in period.
            return await query.CountAsync();
        }

        public async Task<Dictionary<string, int>> GetVotosPorEncuesta(int? alcaldiaId, int top = 5)
        {
            using var context = _dbFactory.CreateDbContext();
            var query = context.Encuestas
                .Where(e => e.Activa)
                .Include(e => e.Votos)
                .AsQueryable();

            if (alcaldiaId.HasValue)
                query = query.Where(e => e.AlcaldiaId == alcaldiaId);

            var data = await query
                .OrderByDescending(e => e.Votos.Count)
                .Take(top)
                .Select(e => new { e.Titulo, Count = e.Votos.Count })
                .ToDictionaryAsync(k => k.Titulo, v => v.Count);

            return data;
        }

        public async Task<List<DateValue>> GetActivityOverTime(int? alcaldiaId, DateTime start, DateTime end)
        {
            using var context = _dbFactory.CreateDbContext();
            
            // Group votes by Date
             var query = context.Votos.AsQueryable();
             if (alcaldiaId.HasValue) query = query.Where(v => v.Encuesta.AlcaldiaId == alcaldiaId);
             
             var endDate = end.Date.AddDays(1);
             query = query.Where(v => v.FechaVoto >= start && v.FechaVoto < endDate);

             var data = await query
                .GroupBy(v => v.FechaVoto.Date)
                .Select(g => new DateValue { Date = g.Key, Value = g.Count() })
                .OrderBy(d => d.Date)
                .ToListAsync();

             return data;
        }
    }

    public class DateValue
    {
        public DateTime Date { get; set; }
        public int Value { get; set; }
    }
}
