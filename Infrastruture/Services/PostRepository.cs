using Aplication.Dtos;
using Aplication.Interfaces;
using Doiman;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastruture.Services;

public class PostRepository:IPostRepository
{
    private readonly DataContext _context;

    public PostRepository(DataContext context)
    {
        _context = context;
    }
    public void  Add(Post post)
    {
       var data=_context.Set<Post>().Add(post);
    }

    public  Post ListById(int Id)
    {
        var post =_context.Post.FirstOrDefault(post1 =>post1.Id == Id); //se nao existe vai retornar null
        return  post;
    }

    public async  Task<int> Complete()
    {
       return await _context.SaveChangesAsync() ;
    }
}