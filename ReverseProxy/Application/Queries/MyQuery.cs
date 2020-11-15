using System;
using System.Collections.Generic;
using MediatR;
using ReverseProxy.Application.Dto;

namespace ReverseProxy.Application.Queries
{
    public class MyQuery : IRequest<List<MyDto>>
    {

    }
}
