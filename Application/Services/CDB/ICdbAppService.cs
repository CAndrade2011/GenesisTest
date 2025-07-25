using Application.Services.CDB.DTO;

namespace Application.Services.CDB;

public interface ICdbAppService
{
    CdbResponse Calcular(CdbCommand request);
} 