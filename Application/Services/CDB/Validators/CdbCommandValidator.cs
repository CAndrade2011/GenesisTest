using Application.Services.CDB.DTO;

namespace Application.Services.CDB.Validators;

public static class CdbCommandValidator
{
    public static bool EhValido(CdbCommand? request, out string? mensagemErro)
    {
        mensagemErro = string.Empty;

        if (request is null)
        {
            mensagemErro += $"{nameof(request)} não pode ser nulo. ";
        }

        if (request is not null && request!.ValorInicial <= 0)
        {
            mensagemErro += $"{nameof(request.ValorInicial)} deve ser maior que zero. ";
        }

        if (request is not null && request!.PrazoMeses < 2)
        {
            mensagemErro += $"{nameof(request.PrazoMeses)} deve ser de no mínimo 2 meses. ";
        }

        return string.IsNullOrWhiteSpace(mensagemErro);
    }
}
