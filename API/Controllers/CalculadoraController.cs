using API.SwaggerExamples;
using Application.Services.CDB;
using Application.Services.CDB.DTO;
using Application.Services.CDB.Validators;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CalculadoraController(ICdbAppService cdbAppService) : ControllerBase
{
    /// <summary>
    /// Calcula o rendimento de um CDB com base no valor inicial e prazo em meses
    /// </summary>
    /// <param name="request">Dados do investimento (valor inicial e prazo em meses)</param>
    /// <returns>Resultado do cálculo com valor bruto e líquido</returns>
    /// <response code="200">Cálculo realizado com sucesso</response>
    /// <response code="400">Parâmetros inválidos</response>
    /// <response code="401">API Key não fornecida ou inválida</response>
    [HttpPost("calcular")]
    [SwaggerRequestExample(typeof(CdbCommand), typeof(CdbCommandExample))]
    [ProducesResponseType(typeof(CdbResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public ActionResult<CdbResponse> Calcular([FromBody] CdbCommand? request)
    {
        if (!CdbCommandValidator.EhValido(request, out var erro))
            return BadRequest(erro);

        var response = cdbAppService.Calcular(request!);
        return Ok(response);
    }
}
