using System;
using System.Collections.Generic;
using MediatR;
using ApiGateway.Application.Dto;

namespace ApiGateway.Application.Queries
{
    public class MyQuery : IRequest<List<MyDto>>
    {

    }
}
