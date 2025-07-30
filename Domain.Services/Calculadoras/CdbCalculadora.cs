using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services.Calculadoras;

public class CdbCalculadora : ICdbCalculadora
{
    private static readonly decimal CDI = 0.009m;
    private static readonly decimal TB = 1.08m;

    public CdbResultadoCalculado Calcular(CdbParametrosParaCalculo parametros)
    {
        decimal valorBruto = parametros.ValorInicial;

        for (int i = 0; i < parametros.PrazoMeses; i++)
            valorBruto *= (1 + (CDI * TB));

        decimal aliquota = ObterAliquota(parametros.PrazoMeses);
        decimal valorGanho = valorBruto - parametros.ValorInicial;
        decimal valorGanhoMenosImposto = valorGanho * (1 - aliquota);
        decimal valorLiquido = parametros.ValorInicial + valorGanhoMenosImposto;

        return new CdbResultadoCalculado(valorBruto, valorLiquido);
    }

    private static decimal ObterAliquota(int meses) => meses switch
    {
        <= 6 => 0.225m,
        <= 12 => 0.20m,
        <= 24 => 0.175m,
        _ => 0.15m
    };
} 