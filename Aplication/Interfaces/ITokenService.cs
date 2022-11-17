using Doiman;

namespace Aplication.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}