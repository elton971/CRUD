using Aplication.Dtos;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;

public class ListPosts
{
    public class ListPostsQuery : IRequest<List<PostDto>>
    {
        //nao colocamos nada aqui poque vai retornar os posts
    }

    public class ListPostsQueryHandler : IRequestHandler<ListPostsQuery, List<PostDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ListPostsQueryHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PostDto>> Handle(ListPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _context.Post.Include(x => x.User).ToListAsync(cancellationToken);
            return _mapper.Map<List<PostDto>>(posts);
        }
    }
}