using Aplication.Dtos;
using Aplication.Users;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController:BaseApiController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
     
        
    //and point para listar as posts
    [HttpGet]
    
    public async Task<ActionResult<List<UserDto>>> GetAllUser()
    {
        //ira buscar a informacao a base de dados
        return await _mediator.Send(new ListUsers.ListUsersQuery());
    }
    //this method return one user.
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById()
    {
        return await _mediator.Send(new ListUserById.ListUserByIdQuery());
    }
    
    [HttpGet("usernames")]
    public async Task<List<string>> GetUserNames()
    {
        return await _mediator.Send(new UserNameList.UserNameListQuery());
    }
   
   
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> CreateUsers(CreateUser.CreateUserCommand command)
    {
        return  await _mediator.Send(command);//retorna um objecto com todos os posts
    }


}