using System;
using System.Collections.Generic;
using MediatR;
using ApiGateway.Application.Dto;

namespace ApiGateway.Application.Commands
{
    public class MyCommand : IRequest<MyDto>
    {

    }
}
