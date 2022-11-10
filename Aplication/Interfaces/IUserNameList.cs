using Doiman;

namespace Aplication.Interfaces;

public interface IUserNameList
{
    public List<string> GetUserNames(List<User> users);
}