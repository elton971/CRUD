using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using Aplication.Posts;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Users;

public class CreateUser
{
     //recebe os dados que vem do mediator na classe PostController
        public class CreateUserCommand :IRequest<UserDto>
        {
            //os dados que eu quero armazenar
            public string Email { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }
            public string UserName { get; set; }
            
            
        }
        

        //onde teremos a logica toda da criacao de um post
        public  class CreateUserCommandHandler :IRequestHandler<CreateUser.CreateUserCommand,UserDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _manager;

            public CreateUserCommandHandler(DataContext context, IMapper mapper,UserManager<User> manager)
            {
                _context = context;
                _mapper = mapper;
                _manager = manager;
            }
            public async Task<UserDto> Handle(CreateUser.CreateUserCommand request, CancellationToken cancellationToken)
            {
                
                var user = new User()
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber
                };

                var result = await _manager.CreateAsync(user, request.Password);
              
               if (result.Succeeded)
               {
                   return _mapper.Map<User, UserDto>(user);
                   
               }
               throw new RestException(HttpStatusCode.Conflict,result.Errors);
              
             
              
            }
        }
}