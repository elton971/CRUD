using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users;

public class ListUser
{
    public class ListUserQuery:IRequest<List<User>>
    {
        
    }
   
    
    public class ListUserQueryHandler: IRequestHandler<ListUserQuery,List<User>>
    {
        private readonly DataContext _context;

        public ListUserQueryHandler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<List<User>> Handle(ListUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }
    }
}