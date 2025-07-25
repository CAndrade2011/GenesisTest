namespace Domain.ValueObjects;

public class CdbParametrosParaCalculo
{
    public decimal ValorInicial { get; }
    public int PrazoMeses { get; }

    public CdbParametrosParaCalculo(decimal valorInicial, int prazoMeses)
    {
        ValorInicial = valorInicial;
        PrazoMeses = prazoMeses;

        Validar();
    }

    private void Validar()
    {
        if (ValorInicial <= 0)
            throw new ArgumentException($"{nameof(ValorInicial)} deve ser maior que zero.");

        if (PrazoMeses < 2)
            throw new ArgumentException($"{nameof(PrazoMeses)} deve ser de no mínimo 2 meses.");
    }
} 