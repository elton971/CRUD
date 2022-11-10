//vai ser usada para create novos posts

using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using AutoMapper;
using Doiman;
using FluentValidation;
using MediatR;
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
            public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;//inicializo com a data atual
            public int UserId { get; set; }
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

            public CreatePostCommandHandle(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                //validacao dos dados
                var postFound=await _context.Post.FirstOrDefaultAsync(post1 => post1.Title == request.Title);//se nao existe vai retornar null

                if (postFound != null)
                {
                    throw new RestException(HttpStatusCode.Conflict,"Error, This post already exist, change the title");
                }
                
                
                var post = new Post()
                {
                    Creationdate = request.CreationDate,
                    Image = request.Image,
                    Title = request.Title,
                };
                
                //add os dados na base de dados.
               await  _context.Post.AddAsync(post, cancellationToken);
               
               //faz commit, para salvar as alteracoes
               int result = await _context.SaveChangesAsync(cancellationToken) ;//vai retornar um valor int
               if (result<0)
               {
                   throw new Exception("AN ERROR OCCURRED");
               }

               return _mapper.Map<Post, PostDto>(post);
                //retorna esse post para o mediator na classe PostsController
            }
        }
        
        
    }
}