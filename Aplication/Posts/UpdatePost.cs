using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;

public class UpdatePost
{
    
    
    public class UpdatePostCommand:IRequest<Post>
    {
        public string Title  { get; set; }
        public string Image  { get; set; }
        public int Id  { get; set; }
    }

    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Post>
    {
        private readonly DataContext _context;

        public UpdatePostCommandHandler(DataContext context)
        {
            _context = context;
        }
        
         public async Task<Post> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
         {
                    var post = await _context.Post.FirstOrDefaultAsync(user=>user.Id==request.Id);
        
                    if (post==null)
                    {
                        throw new Exception("User not found");
                    }
                    
                    post.Image = request.Image;
                    post.Title = request.Title;
                    //_context.Entry<>
                    
                    await _context.SaveChangesAsync();

                    return post;

         }
    }

}