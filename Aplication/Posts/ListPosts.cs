using Aplication.Dtos;
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

    public class ListPostsQueryHabdler : IRequestHandler<ListPostsQuery, List<PostDto>>
    {
        private readonly DataContext _context;

        public ListPostsQueryHabdler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<PostDto>> Handle(ListPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _context.Post.Include(x => x.User).ToListAsync(cancellationToken);
            var postListDto = posts.Select(post => new PostDto
            {
                Id = post.Id,
                Creationdate = post.Creationdate,
                Image = post.Image,
                Title = post.Title,
                UserName = post.User.Username
            }).ToList();

            return postListDto;
        }
    }
}