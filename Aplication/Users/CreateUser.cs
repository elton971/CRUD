using Aplication.DTO;
using AutoMapper;
using Doiman;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users;

public class CreateUser
{
    public class CreateUserCommand: IRequest<UserDTO>
    {
      
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
    }
    
    public class CreateUserHandler: IRequestHandler<CreateUserCommand,UserDTO>
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CreateUserHandler(DataContext context,UserManager<User> userManager,IMapper mapper)//UserManager ajuda na manipulacao do utilizador
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
        
        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = await _context.Users.FirstOrDefaultAsync(user=>user.UserName==request.Username,cancellationToken);

            if (validator!=null)
            {
                throw new Exception("User exists");
            }

            var user = new User()
            {
                Fullname = request.Name,
                UserName = request.Username,
                Email=request.Email
            };
            
            //incriptamos o passord e cria add na base de dados
            var result=await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return _mapper.Map<User, UserDTO>(user);
            }
            throw new Exception("Error creating user");
        }
    }

    
    
}