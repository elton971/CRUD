using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;

public class ListPosts
{
    public class ListPostsQuery : IRequest<List<Post>>
    {
        //nao colocamos nada aqui poque vai retornar os posts
        
    }

    public class ListPostsQueryHabdler : IRequestHandler<ListPostsQuery, List<Post>>
    {
        private readonly DataContext _context;

        public ListPostsQueryHabdler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<List<Post>> Handle(ListPostsQuery request, CancellationToken cancellationToken)
        {
           return await _context.Post.ToListAsync(cancellationToken) ;
        }
    }
}