using Application.Services.CDB;
using Application.Services.CDB.DTO;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Application.Tests.Services.CDB;

[Trait("Category", "CdbAppService")]
public class CdbAppServiceTests
{
    [Fact]
    [Trait("CdbAppService", "Resultado Esperado")]
    public void Calcular_DeveRetornarResultadoEsperado()
    {
        // Arrange
        var mockCalculadora = new Mock<ICdbCalculadora>();
        mockCalculadora
            .Setup(c => c.Calcular(It.IsAny<CdbParametrosParaCalculo>()))
            .Returns(new CdbResultadoCalculado(1100, 900));

        var appService = new CdbAppService(mockCalculadora.Object);
        var command = new CdbCommand(1000, 6);

        // Act
        var resultado = appService.Calcular(command);

        // Assert
        resultado.ValorBruto.Should().Be(1100);
        resultado.ValorLiquido.Should().Be(900);
    }

    [Theory]
    [Trait("CdbAppService", "Parâmetros Inválidos")]
    [InlineData(0, 6)]
    [InlineData(1000, 1)]
    [InlineData(-1, 6)]
    [InlineData(1000, -1)]
    public void Calcular_DeveLancarArgumentException_SeParametrosInvalidos(decimal valor, int meses)
    {
        // Arrange
        var mockCalculadora = new Mock<ICdbCalculadora>();
        var appService = new CdbAppService(mockCalculadora.Object);
        var command = new CdbCommand(valor, meses);

        // Act
        Action act = () => appService.Calcular(command);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}
