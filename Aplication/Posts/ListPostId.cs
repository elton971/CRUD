using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;

public class ListPostId
{
    public class ListPostIdQuery :IRequest<Post>
    {
        public int Id { get; set; }
       
    }

    public class ListPostByIdHandler : IRequestHandler<ListPostIdQuery, Post>
    {
        private readonly DataContext _context;

        public ListPostByIdHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Post> Handle(ListPostIdQuery request, CancellationToken cancellationToken)
        {
            //validacao dos dados
            var post =
                await _context.Post.FirstOrDefaultAsync(post1 =>
                    post1.Id == request.Id); //se nao existe vai retornar null

            if (post == null)
            {
                throw new Exception("Error, Post doens exist!!");
            }

            return post;
        }
    }
}