using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReverseProxy.Application.Dto;

namespace ReverseProxy.Persistence.DbService
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
