using Domain.Services.Calculadoras;
using Domain.ValueObjects;

namespace Domain.Services.Tests.Calculadoras;

[Trait("Category", "Calculadora")]
public class CdbCalculadoraTests
{

    [Trait("CdbCalculadora", "Cálculo Bruto e Líquido")]
    [Theory]
    [InlineData(1000, 6)]
    [InlineData(1000, 12)]
    [InlineData(1000, 24)]
    [InlineData(1000, 25)]
    public void DeveCalcular_ValorBrutoELiquido_Corretamente(decimal valorInicial, int meses)
    {
        // Arrange
        var calculadora = new CdbCalculadora();
        var parametros = new CdbParametrosParaCalculo(valorInicial, meses);

        // Act
        var resultado = calculadora.Calcular(parametros);

        // Assert
        Assert.True(resultado.ValorBruto > valorInicial);
        Assert.True(resultado.ValorLiquido < resultado.ValorBruto);
    }

    [Trait("CdbCalculadora", "Valor Pequeno")]
    [Fact]
    public void DeveManterPrecisao_ComValorPequeno()
    {
        var calculadora = new CdbCalculadora();
        var parametros = new CdbParametrosParaCalculo(10, 3);
        var resultado = calculadora.Calcular(parametros);

        Assert.True(resultado.ValorBruto > 10);
        Assert.InRange(resultado.ValorLiquido, 10.1m, 10.5m);
    }

    [Trait("CdbCalculadora", "Valores Grandes")]
    [Fact]
    public void DeveManterPrecisao_ParaValoresGrandes()
    {
        var calculadora = new CdbCalculadora();
        var parametros = new CdbParametrosParaCalculo(1000000, 36);

        var resultado = calculadora.Calcular(parametros);

        Assert.True(resultado.ValorBruto > 1000000);
        Assert.True(resultado.ValorLiquido < resultado.ValorBruto);
    }

    [Trait("CdbCalculadora", "Aliquota Correta")]
    [Theory]
    [InlineData(6, 0.225)]
    [InlineData(12, 0.20)]
    [InlineData(24, 0.175)]
    [InlineData(36, 0.15)]
    public void DeveAplicarAliquotaCorreta(int meses, decimal aliquotaEsperada)
    {
        var calculadora = new CdbCalculadora();
        var parametros = new CdbParametrosParaCalculo(1000, meses);

        var resultado = calculadora.Calcular(parametros);
        var valorBruto = resultado.ValorBruto;
        var valorLiquido = resultado.ValorLiquido;
        var valorInicial = parametros.ValorInicial;
        
        // A alíquota é aplicada apenas sobre o ganho
        var valorGanho = valorBruto - valorInicial;
        var valorGanhoLiquido = valorLiquido - valorInicial;
        var aliquotaCalculada = Math.Round(1 - (valorGanhoLiquido / valorGanho), 3);

        Assert.Equal(aliquotaEsperada, aliquotaCalculada, 3);
    }
} 