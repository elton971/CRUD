using Aplication.Dtos;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Users;

public class ListUsers
{
    public class ListUsersQuery : IRequest<List<UserDto>>
    {
       
    }
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, List<UserDto>>
    {
        
       
        private readonly UserManager<User> _manager;
        private readonly IMapper _mapper;

        public ListUsersQueryHandler(  UserManager<User> manager,IMapper mapper)
        {
            
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var userList = await _manager.Users.ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<UserDto>>(userList);
        }
    }
}