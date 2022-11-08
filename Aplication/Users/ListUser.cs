using System.Security.Cryptography;
using Aplication.DTO;
using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users;

public class ListUser
{
    public class ListUserQuery:IRequest<List<UserDTO>>
    {
        
    }
   
    
    public class ListUserQueryHandler: IRequestHandler<ListUserQuery,List<UserDTO>>
    {
        private readonly DataContext _context;

        public ListUserQueryHandler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<List<UserDTO>> Handle(ListUserQuery request, CancellationToken cancellationToken)
        {
            var users= await _context.Users.ToListAsync(cancellationToken);
            List<UserDTO> userDTOList;
            userDTOList=users.Select(user=> new UserDTO
            {
                Id = int.Parse(user.Id),
                Name = user.UserName
            }).ToList();
            return userDTOList;
        }
    }
}