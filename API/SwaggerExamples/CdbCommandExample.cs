using Application.Services.CDB.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples;

public class CdbCommandExample : IExamplesProvider<CdbCommand>
{
    public CdbCommand GetExamples() => new(1000, 12);
} 