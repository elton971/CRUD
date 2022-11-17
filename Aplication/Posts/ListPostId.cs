using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using Aplication.Interfaces;
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
        private readonly IPostRepository _postRepository;

        public ListPostByIdHandler(DataContext context, IMapper mapper, IPostRepository postRepository)
        {
            _context = context;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<PostDto> Handle(ListPostIdQuery request, CancellationToken cancellationToken)
        {
            var post =  _postRepository.ListById(request.Id);
            if (post == null)
            {
                throw new RestException(HttpStatusCode.Conflict,"Error, Post doens exist!!");
            }

            return _mapper.Map<PostDto>(post);
        }
    }
}