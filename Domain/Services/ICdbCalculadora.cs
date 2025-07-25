using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Services;

public interface ICdbCalculadora
{
    CdbResultadoCalculado Calcular(CdbParametrosParaCalculo parametros);
} 