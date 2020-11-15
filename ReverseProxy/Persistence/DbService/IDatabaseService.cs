using System.Collections.Generic;
using System.Threading.Tasks;
using ReverseProxy.Application.Dto;

namespace ReverseProxy.Persistence.DbService
{
    public interface IDatabaseService
    {
        Task<List<MyDto>> GetData();
    }
}
