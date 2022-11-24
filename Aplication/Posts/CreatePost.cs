//vai ser usada para create novos posts

using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using Aplication.Interfaces;
using AutoMapper;
using Doiman;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts
{
    public class CreatePost
    {
        //recebe os dados que vem do mediator na classe PostController
        public class CreatePostCommand :IRequest<PostDto>
        {
            //os dados que eu quero armazenar
            public string Title { get; set; }
            public string Image { get; set; }
            public string Content { get; set; }
            
            
        }

        //classe para validarmos os dados que vem do da Classe CreatePostcommand
        public class CreatePostValidator :AbstractValidator<CreatePostCommand>
        {
            public CreatePostValidator()
            {
                RuleFor(x=>x.Image).NotEmpty();
                RuleFor(x=>x.Title).NotEmpty();
                
            }
        }
        
        //onde teremos a logica toda da criacao de um post
        public  class CreatePostCommandHandle :IRequestHandler<CreatePostCommand,PostDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IPostRepository _postRepository;
            private readonly UserManager<User> _userManager;
            private readonly IUserAcessor _userAcessor;


            public CreatePostCommandHandle(DataContext context, IMapper mapper,IPostRepository postRepository,UserManager<User> userManager,
                IUserAcessor userAcessor)
            {
                _context = context;
                _mapper = mapper;
                _postRepository = postRepository;
                _userManager=userManager;
                _userAcessor = userAcessor;
            }
            public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                //validacao dos dados
                var postFound=await _context.Post.FirstOrDefaultAsync(post1 => post1.Title == request.Title);//se nao existe vai retornar null
                var user = await _userManager.FindByIdAsync(_userAcessor.GetCurrentUserId());
                if (postFound != null)
                {
                    throw new RestException(HttpStatusCode.Conflict,"Error, This post already exist, change the title");
                }

                if (user == null)
                {
                    throw new RestException(HttpStatusCode.Conflict,"Error, This user don't exist");
                }
                
                
                var post = new Post()
                {
                    Creationdate = DateTime.UtcNow,
                    Image = request.Image,
                    Title = request.Title,
                    Content = request.Content,
                    User = user
                }; 
                
                _postRepository.Add(post);
                var result = await _postRepository.Complete()<0;
                if (result)
                {
                    throw new Exception("AN ERROR OCCURRED");
                }
                return _mapper.Map<Post, PostDto>(post);
               

            }
        }
        
        
    }
}