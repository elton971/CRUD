using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;

public class UpdatePost
{
    public class UpdatePostCommand: IRequest<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
    public class UpdatePostHandler: IRequestHandler<UpdatePostCommand, Post>
    {
        private readonly DataContext _context;

        public UpdatePostHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<Post> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Post.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (post is null)
            {
                throw new Exception("Post Not Found");
            }

            post.Image = request.Image;
            post.Title = request.Title;

            _context.Post.Update(post);
            
            var result = await _context.SaveChangesAsync(cancellationToken) < 0;
            if (result)
            {
                throw new Exception("An Error Occurred while updating");
            }

            return post;
        }
    }
}