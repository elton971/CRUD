using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users;

public class ListUserById
{
    
    public class ListUserByIdQuery: IRequest<User>
    {
        public int Id { get; set; }
    }
    
    public class  ListUserByIdQueryHandler:IRequestHandler<ListUserByIdQuery,User>
    {
        private readonly DataContext _context;

        public ListUserByIdQueryHandler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<User> Handle(ListUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => int.Parse(user.Id)==request.Id );
            if (user==null)
            {
                throw new Exception("user not found");
            }
            return user;
        }
        
        
    }
}