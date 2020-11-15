using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Application.Dto;

namespace ApiGateway.Persistence.DbService
{
    public class DatabaseService : IDatabaseService
    {
        public async Task<List<MyDto>> GetData()
        {
            await Task.FromResult(1);
            return new List<MyDto>();
        }
    }
}
