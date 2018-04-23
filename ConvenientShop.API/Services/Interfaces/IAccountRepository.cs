using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IAccountRepository
    {
        bool AuthorizeUser(int userId, Permission perm);
        (bool isSuccessful, int accountId) LogIn(string username, string password);
        bool IsUsernameExists(string username);
        void CreateAccount(string username, string password, int roleId);
    }
}
