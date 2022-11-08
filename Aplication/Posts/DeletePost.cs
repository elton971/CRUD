using Application.Users;
using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;

public class DeletePost
{
    public class DeletePostCommand: IRequest<Post>
    {
        public int Id { get; set; }
    }

   

    public class DeletePostCommandHandler: IRequestHandler<DeletePost.DeletePostCommand, Post>
    {
        private readonly DataContext _context;

        public DeletePostCommandHandler(DataContext context)
        {
            _context = context;
        }
        
        public async Task<Post> Handle(DeletePost.DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Post.FirstOrDefaultAsync(user => user.Id == request.Id);

            if (post == null)
            {
                throw new Exception("User not found");
            }
            
            //here we remove de user on our database
            _context.Post.Remove(post);
            //we make commit and save the changes
            var delete = await _context.SaveChangesAsync(cancellationToken) <= 0;
             
            //here we verify that we save the changes sucessefull or we have a error
            if (delete)
            {
                throw new Exception("Error deleting user");
            }
            return post; //return the user to mediator services.
        }
    }
}