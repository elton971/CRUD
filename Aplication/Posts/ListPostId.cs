using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;

public class ListPostId
{
    public class ListPostIdQuery :IRequest<PostDto>
    {
        public int Id { get; set; }
       
    }

    public class ListPostByIdHandler : IRequestHandler<ListPostIdQuery, PostDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ListPostByIdHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(ListPostIdQuery request, CancellationToken cancellationToken)
        {
            //validacao dos dados
            var post =
                await _context.Post.FirstOrDefaultAsync(post1 =>
                    post1.Id == request.Id); //se nao existe vai retornar null

            if (post == null)
            {
                throw new RestException(HttpStatusCode.Conflict,"Error, Post doens exist!!");
            }

            return _mapper.Map<PostDto>(post);
        }
    }
}