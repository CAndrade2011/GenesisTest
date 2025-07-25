using Domain.ValueObjects;

namespace Domain.Tests.ValueObjects;

[Trait("Category", "CdbParametrosParaCalculo")]
public class CdbParametrosParaCalculoTests
{
    [Trait("CdbParametrosParaCalculo", "Parâmetros Inválidos")]
    [Theory]
    [InlineData(0, 5)]
    [InlineData(-100, 10)]
    [InlineData(1000, 1)]
    [InlineData(0, 0)]
    public void Construtor_DeveLancarExcecao_SeParametrosInvalidos(decimal valorInicial, int prazoMeses)
    {
        var ex = Assert.Throws<ArgumentException>(() => new CdbParametrosParaCalculo(valorInicial, prazoMeses));
        Assert.NotNull(ex.Message);
    }

    [Trait("CdbParametrosParaCalculo", "Parâmetros Válidos")]
    [Fact]
    public void Construtor_DeveCriarInstancia_SeParametrosValidos()
    {
        var obj = new CdbParametrosParaCalculo(1000, 12);

        Assert.Equal(1000, obj.ValorInicial);
        Assert.Equal(12, obj.PrazoMeses);
    }
} 