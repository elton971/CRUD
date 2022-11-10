using Aplication.Interfaces;
using Doiman;

namespace Infrastruture.Services;

public class UserList:IUserNameList
{
    public List<string> GetUserNames(List<User> users)
    {
        return users.Select(x => x.UserName).ToList();
    }
}