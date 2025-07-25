using Application.Services.CDB.DTO;
using Domain.Services;
using Domain.ValueObjects;

namespace Application.Services.CDB;

public class CdbAppService : ICdbAppService
{
    private readonly ICdbCalculadora _calculadora;

    public CdbAppService(ICdbCalculadora calculadora)
    {
        _calculadora = calculadora;
    }

    public CdbResponse Calcular(CdbCommand request)
    {
        var parametros = new CdbParametrosParaCalculo(request.ValorInicial, request.PrazoMeses);
        var resultado = _calculadora.Calcular(parametros);

        return new CdbResponse(resultado.ValorBruto, resultado.ValorLiquido);
    }
} 