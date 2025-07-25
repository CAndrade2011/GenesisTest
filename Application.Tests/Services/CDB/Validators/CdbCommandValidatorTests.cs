using Application.Services.CDB.DTO;
using Application.Services.CDB.Validators;
using FluentAssertions;

namespace Application.Tests.Services.CDB.Validators;

[Trait("Category", "CdbCommandValidator")]
public class CdbCommandValidatorTests
{
    [Theory]
    [Trait("CdbCommandValidator", "Parâmetros Inválidos")]
    [InlineData(0, 6)]
    [InlineData(-100, 6)]
    [InlineData(1000, 1)]
    [InlineData(1000, -1)]
    [InlineData(-1000, -1)]
    [InlineData(0, 0)]
    public void EhValido_DeveRetornarFalse_ParaParametrosInvalidos(decimal valorInicial, int prazoMeses)
    {
        // Arrange
        var command = new CdbCommand(valorInicial, prazoMeses);

        // Act
        var valido = CdbCommandValidator.EhValido(command, out var mensagem);

        // Assert
        valido.Should().BeFalse();
        mensagem.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("CdbCommandValidator", "Command Nulo")]
    public void EhValido_DeveRetornarFalse_SeCommandForNulo()
    {
        // Act
        var valido = CdbCommandValidator.EhValido(null, out var mensagem);

        // Assert
        valido.Should().BeFalse();
        mensagem.Should().Be("request não pode ser nulo. ");
    }

    [Fact]
    [Trait("CdbCommandValidator", "Parametros Válidos")]
    public void EhValido_DeveRetornarTrue_ParaParametrosValidos()
    {
        // Arrange
        var command = new CdbCommand(1000, 12);

        // Act
        var valido = CdbCommandValidator.EhValido(command, out var mensagem);

        // Assert
        valido.Should().BeTrue();
        mensagem.Should().BeEmpty();
    }
} 