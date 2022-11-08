using Aplication.DTO;
using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;

public class ListPosts
{
    public class ListPostsQuery : IRequest<List<PostDTO>>
    {
        //nao colocamos nada aqui poque vai retornar os posts
        
    }

    public class ListPostsQueryHandler : IRequestHandler<ListPostsQuery, List<PostDTO>>
    {
        private readonly DataContext _context;

        public ListPostsQueryHandler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<List<PostDTO>> Handle(ListPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _context.Post.Include(x => x.User).ToListAsync(cancellationToken); 
           var postListDto=posts.Select(post=> new PostDTO
           {
               Id = post.Id,
               Creationdate = post.Creationdate,
               Image = post.Image,
               Title = post.Title,
               UserName = post.User.UserName
           }).ToList();
           return postListDto;
        }
    }
}