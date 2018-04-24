using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Models;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IAccountRepository
    {
        bool AuthorizeUser(int userId, Permission perm);
        bool IsUsernameExists(string username);
        void CreateAccount(string username, string password, int roleId);
        (bool, AccountDto) LogIn(string username, string password);
    }
}
