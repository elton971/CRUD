using Aplication.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Users;

public class ListUser
{
    public class ListUserQuery:IRequest<List<UserDto>>
    {
        
    }
   
    
    public class ListUserQueryHandler: IRequestHandler<ListUserQuery,List<UserDto>>
    {
        private readonly DataContext _context;

        public ListUserQueryHandler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<List<UserDto>> Handle(ListUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);

            var usersListDto = users.Select(user => new UserDto { Id = user.Id, Name = user.Name }).ToList();
            
            /*
             users.ForEach(user =>
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name
                };
                usersListDto.Add(userDto);
            });
            */
            return usersListDto;
        }
    }
}