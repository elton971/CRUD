using Doiman;

namespace Aplication.Interfaces;

public interface IPostRepository
{
    void Add(Post post);
    
    /*Post Update(Post post);
    Post Delete(int Id);
    List<Post>ListAll();*/

    Post ListById(int Id);

    Task<int> Complete();
}