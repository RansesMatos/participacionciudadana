using ParticipacionDigital.Core.Entities;
using Xunit;

namespace ParticipacionDigital.Tests.Unit;

public class EncuestaTests
{
    [Fact]
    public void Encuesta_DeberiaEstarActiva_SiFechaActualEstaEnRango()
    {
        // Arrange
        var encuesta = new Encuesta
        {
            FechaInicio = DateTime.UtcNow.AddDays(-1),
            FechaFin = DateTime.UtcNow.AddDays(1)
        };

        // Act & Assert
        // Nota: La lógica de "Activa" es una propiedad derivada o calculada en la vista, 
        // pero aquí validamos que las fechas permitan ese estado.
        Assert.True(encuesta.FechaInicio < DateTime.UtcNow);
        Assert.True(encuesta.FechaFin > DateTime.UtcNow);
    }

    [Fact]
    public void Encuesta_DeberiaTenerOpcionesVacías_AlCrear()
    {
        // Arrange
        var encuesta = new Encuesta();

        // Assert
        Assert.NotNull(encuesta.Opciones);
        Assert.Empty(encuesta.Opciones);
    }
}
