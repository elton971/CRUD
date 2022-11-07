using Doiman;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users;

public class CreateUser
{
    public class CreateUserCommand: IRequest<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Username { get; set; }
        public string Password { get; set; }

    }
    
    public class CreateUserHandler: IRequestHandler<CreateUserCommand,User>
    {
        private readonly DataContext _context;

        public CreateUserHandler(DataContext context)
        {
            _context = context;
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
        
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
                Password = request.Password
            };

            await _context.Users.AddAsync(user);
            
            var result=await _context.SaveChangesAsync(cancellationToken)<0;

            if (result)
            {
                throw new Exception("Error creating user");
            }

            return user;

        }
    }

    
    
}