using Application.Users;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController:BaseApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUser.CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUser()
    {
        return await _mediator.Send(new ListUser.ListUserQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        return  await _mediator.Send(new ListUserById.ListUserByIdQuery{Id = id});
    }

    [HttpDelete ("{id}")]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
        return await _mediator.Send(new DeleteUser.DeleteUserCommand{Id = id});
    }
    
    [HttpPut ("{id}")]
    public async Task<ActionResult<User>> UpdateUser(UpdateUser.UpdateUserCommand command,int id)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }
}