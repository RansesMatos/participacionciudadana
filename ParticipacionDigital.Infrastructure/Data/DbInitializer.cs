using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParticipacionDigital.Core.Entities;
using ParticipacionDigital.Core.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace ParticipacionDigital.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<Usuario> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            // Ensure Database is Created
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();

            // Seed Alcaldias
            if (!context.Alcaldias.Any())
            {
                var alcaldias = new List<Alcaldia>
                {
                    new Alcaldia { Nombre = "Distrito Nacional", Region = "Gran Santo Domingo" },
                    new Alcaldia { Nombre = "Santo Domingo Este", Region = "Gran Santo Domingo" },
                    new Alcaldia { Nombre = "Santo Domingo Norte", Region = "Gran Santo Domingo" },
                    new Alcaldia { Nombre = "Santiago de los Caballeros", Region = "Cibao" },
                    new Alcaldia { Nombre = "La Vega", Region = "Cibao" },
                    new Alcaldia { Nombre = "San Cristóbal", Region = "Sur" },
                    new Alcaldia { Nombre = "San Pedro de Macorís", Region = "Este" },
                    new Alcaldia { Nombre = "Higüey", Region = "Este" }
                };
                context.Alcaldias.AddRange(alcaldias);
                await context.SaveChangesAsync();
            }

            // Seed Roles
            string[] roles = Enum.GetNames(typeof(RolUsuario));
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            // Seed Users
            await SeedUser(userManager, "admin@participacion.gob.do", "Admin123!", RolUsuario.Administrador, "Administrador Principal");
            await SeedUser(userManager, "moderador@participacion.gob.do", "Moderador123!", RolUsuario.Moderador, "Moderador del Foro");
            await SeedUser(userManager, "asistente@participacion.gob.do", "Asistente123!", RolUsuario.Asistente, "Asistente General");
            await SeedUser(userManager, "ciudadano@gmail.com", "Ciudadano123!", RolUsuario.Ciudadano, "Juan Ciudadano");
        }

        private static async Task SeedUser(UserManager<Usuario> userManager, string email, string password, RolUsuario rol, string nombre)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new Usuario
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    Nombre = nombre,
                    Rol = rol,
                    Activo = true,
                    FechaRegistro = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, rol.ToString());
                }
            }
        }
    }
}
