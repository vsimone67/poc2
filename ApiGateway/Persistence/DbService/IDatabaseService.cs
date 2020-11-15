using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Application.Dto;

namespace ApiGateway.Persistence.DbService
{
    public interface IDatabaseService
    {
        Task<List<MyDto>> GetData();
    }
}
