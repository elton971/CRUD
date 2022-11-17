using System.Net;
using Aplication.Dtos;
using Aplication.Errors;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;

public class UpdatePost
{
    public class UpdatePostCommand: IRequest<PostDto>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        
        public string Content { get; set; }
    }
    public class UpdatePostHandler: IRequestHandler<UpdatePostCommand, PostDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UpdatePostHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Post.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (post is null)
            {
                throw new RestException(HttpStatusCode.Conflict,"Error, Post doens exist!!");
            }

            post.Image = request.Image;
            post.Title = request.Title;
            post.Content = request.Content;

            _context.Post.Update(post);
            
            var result = await _context.SaveChangesAsync(cancellationToken) < 0;
            if (result)
            {
                throw new Exception("An Error Occurred while updating");
            }

            return _mapper.Map<PostDto>(post);
        }
    }
}