using System.Linq.Expressions;
using Aplication.DTO;
using AutoMapper;
using Doiman;

namespace Aplication.Helpers.MappingProfiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        //criacao de uma regra de mapeamonto
        //estamos a criar um mapeamento para o username porque é a unica variavel
        //que nao  encontra um nome igual na class Post
        CreateMap<Post,PostDTO>().ForMember(dto=>dto.UserName,
            expression=>
                expression.MapFrom(
                    post=>post.User.UserName));
    }
}