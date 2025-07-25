using API.Controllers;
using Application.Services.CDB;
using Application.Services.CDB.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Tests.Controllers;

[Trait("Category", "CalculadoraController")]
public class CalculadoraControllerTests
{
    private readonly Mock<ICdbAppService> _mockAppService;
    private readonly CalculadoraController _controller;

    public CalculadoraControllerTests()
    {
        _mockAppService = new Mock<ICdbAppService>();
        _controller = new CalculadoraController(_mockAppService.Object);
    }

    [Fact]
    [Trait("CalculadoraController", "Parâmetros Válido")]
    public void Calcular_DeveRetornarOk_SeParametrosValidos()
    {
        // Arrange
        var request = new CdbCommand(1000, 12);
        var expectedResponse = new CdbResponse(1100, 900);
        _mockAppService
            .Setup(x => x.Calcular(request))
            .Returns(expectedResponse);

        // Act
        var result = _controller.Calcular(request);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();

        var okResult = result.Result as OkObjectResult;
        okResult!.StatusCode.Should().Be(200);

        var response = okResult.Value.Should().BeOfType<CdbResponse>().Subject;
        response.ValorBruto.Should().Be(1100);
        response.ValorLiquido.Should().Be(900);
    }

    [Theory]
    [Trait("CalculadoraController", "Parâmetros Inválidos")]
    [InlineData(0, 12)]
    [InlineData(1000, 1)]
    [InlineData(-1, 12)]
    [InlineData(1000, -1)]
    [InlineData(0, 0)]
    [InlineData(-1000, -1)]
    public void Calcular_DeveRetornarBadRequest_SeParametrosInvalidos(decimal valor, int meses)
    {
        // Arrange
        var request = new CdbCommand(valor, meses);

        // Act
        var result = _controller.Calcular(request);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();

        var badRequest = result.Result as BadRequestObjectResult;
        badRequest!.StatusCode.Should().Be(400);
        badRequest.Value.Should().NotBeNull().And.BeOfType<string>();
    }

    [Fact]
    [Trait("CalculadoraController", "Request Nulo")]
    public void Calcular_DeveRetornarBadRequest_SeRequestForNulo()
    {
        // Act
        var result = _controller.Calcular(null);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();

        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult!.StatusCode.Should().Be(400);
        badRequestResult.Value.Should().NotBeNull().And.BeOfType<string>();
        ((string)badRequestResult.Value!).Should().Contain("nulo");
    }

}