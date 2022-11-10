using Aplication.Dtos;
using Aplication.Users;
using AutoMapper;
using Doiman;

namespace Aplication.Helpers.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
      CreateMap<Post, PostDto>().
                 ForMember(dto => dto.UserName, 
                 expression => expression.MapFrom(post => post.User.UserName));  
      
      CreateMap<User,UserDto>();

    }
}