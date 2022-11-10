using Aplication.Interfaces;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Users;

public class UserNameList
{
    public class UserNameListQuery : IRequest<List<string>>
    {
    
    } 
    
    public class UserNameListHandler : IRequestHandler<UserNameListQuery,List<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserNameList _userNameList;

        public UserNameListHandler(UserManager<User> userManager, IUserNameList userNameList)
        {
            _userManager = userManager;
            _userNameList = userNameList;
        }
        public async Task<List<string>> Handle(UserNameListQuery request, CancellationToken cancellationToken)
        {
            var usernameList = await _userManager.Users.ToListAsync(cancellationToken);
            return _userNameList.GetUserNames(usernameList);
        }
    } 
    
}