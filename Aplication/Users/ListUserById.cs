using Aplication.Dtos;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplication.Users;

public class ListUserById
{
    public class ListUserByIdQuery : IRequest<UserDto>
    {
        public string Id { get; set; }
    }

    public class ListUserByIdHandler : IRequestHandler<ListUserByIdQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _manager;

        public ListUserByIdHandler(IMapper mapper, UserManager<User> manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        public  async Task<UserDto> Handle(ListUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user= await _manager.FindByIdAsync(request.Id);
            if (user is null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<UserDto>(user);
            
            
        }
    }
}