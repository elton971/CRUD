using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users;

public class UpdateUser
{
    public class UpdateUserCommand: IRequest<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
       


    }
    
    
    public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand,User>
    {
        private readonly DataContext _context;

        public UpdateUserCommandHandler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user=>int.Parse(user.Id)==request.Id);

            if (user==null)
            {
                throw new Exception("User not found");
            }
            
            user.Fullname = request.Name;
            user.UserName = request.Username;
            //_context.Entry<>
            await _context.SaveChangesAsync();
            
            return user;
            
        }
        
        
    }
}