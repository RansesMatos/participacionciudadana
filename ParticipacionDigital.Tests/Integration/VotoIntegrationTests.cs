using Microsoft.EntityFrameworkCore;
using ParticipacionDigital.Core.Entities;
using ParticipacionDigital.Infrastructure.Data;
using Xunit;

namespace ParticipacionDigital.Tests.Integration;

public class VotoIntegrationTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options); // Asegúrate de que tu DbContext tenga este constructor
    }

    [Fact]
    public async Task GuardarVoto_DeberiaPersistirEnBaseDeDatos()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var encuestaId = 1;
        var opcionId = 1;
        var usuarioId = 1;

        var voto = new Voto
        {
            EncuestaId = encuestaId,
            OpcionId = opcionId,
            UsuarioId = usuarioId,

        };

        // Act
        context.Votos.Add(voto);
        await context.SaveChangesAsync();

        // Assert
        var votoGuardado = await context.Votos.FirstOrDefaultAsync();
        Assert.NotNull(votoGuardado);
        Assert.Equal(usuarioId, votoGuardado.UsuarioId);
    }

    [Fact]
    public async Task EvitarDuplicados_DeberiaPoderValidarseConAnyAsync()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var encuestaId = 1;
        var usuarioId = 99;

        context.Votos.Add(new Voto { EncuestaId = encuestaId, UsuarioId = usuarioId });
        await context.SaveChangesAsync();

        // Act
        var yaVoto = await context.Votos.AnyAsync(v => v.EncuestaId == encuestaId && v.UsuarioId == usuarioId);

        // Assert
        Assert.True(yaVoto);
    }
}
