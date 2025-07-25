using API.SwaggerExamples;
using Application.Services.CDB.DTO;
using Xunit;

namespace API.Tests.SwaggerExamples
{
    public class CdbCommandExampleTests
    {
        [Fact]
        public void GetExamples_DeveRetornarExemploEsperado()
        {
            // Arrange
            var example = new CdbCommandExample();

            // Act
            var result = example.GetExamples();

            // Assert
            Assert.Equal(1000, result.ValorInicial);
            Assert.Equal(12, result.PrazoMeses);
        }
    }
} 