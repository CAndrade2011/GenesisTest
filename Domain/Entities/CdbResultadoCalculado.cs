namespace Domain.Entities;

public class CdbResultadoCalculado
{
    public decimal ValorBruto { get; }
    public decimal ValorLiquido { get; }

    public CdbResultadoCalculado(decimal bruto, decimal liquido)
    {
        ValorBruto = Math.Round(bruto, 2);
        ValorLiquido = Math.Round(liquido, 2);
    }
} 