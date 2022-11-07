using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users;

public class DeleteUser
{
    public class DeleteUserCommand: IRequest<User>
    {
        public int Id { get; set; }
    }

   

    public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, User>
    {
        private readonly DataContext _context;

        public DeleteUserCommandHandler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.Id);

            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            //here we remove de user on our database
            _context.Users.Remove(user);
            //we make commit and save the changes
            var delete = await _context.SaveChangesAsync(cancellationToken) <= 0;
             
            //here we verify that we save the changes sucessefull or we have a error
            if (delete)
            {
                throw new Exception("Error deleting user");
            }
            return user; //return the user to mediator services.
        }
    }
}