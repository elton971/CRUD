using Aplication.Accouts;
using Aplication.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccoutsController:BaseApiController
{
    private readonly IMediator _mediator;

    public AccoutsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login(Login.LoginCommand loginDto)
    {
        return await _mediator.Send(loginDto);
    }
}