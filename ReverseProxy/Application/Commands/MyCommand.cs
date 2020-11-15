using System;
using System.Collections.Generic;
using MediatR;
using ReverseProxy.Application.Dto;

namespace ReverseProxy.Application.Commands
{
    public class MyCommand : IRequest<MyDto>
    {

    }
}
