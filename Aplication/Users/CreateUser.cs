using Aplication.Dtos;
using AutoMapper;
using Doiman;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Users;

public class CreateUser
{
    public class CreateUserCommand: IRequest<UserDto>
    {
        public string Name { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public string Password { get; set; }
        

    }
    
    public class CreateUserHandler: IRequestHandler<CreateUserCommand,UserDto>
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CreateUserHandler(DataContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }
        
        public class  CreateUserValidator: AbstractValidator<CreateUserCommand>
        {
            public CreateUserValidator()
            {
                RuleFor(x=>x.Name).NotEmpty();
                RuleFor(x=>x.Username).NotEmpty();
                RuleFor(x=>x.Password).NotEmpty();
                
                
            }
        }
        
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = await _context.Users.FirstOrDefaultAsync(user=>user.Username==request.Username,cancellationToken);

            if (validator!=null)
            {
                throw new Exception("User exists");
            }

            var user = new User()
            {
                Name = request.Name,
                Username = request.Username,
                // Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            
            if (result.Succeeded)
            {
                return _mapper.Map<User, UserDto>(user);
            }

            throw new Exception(result.Errors.ToString());
            
        }
    }

    
    
}