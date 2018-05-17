using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IAccountRepository
    {
        bool AuthorizeUser(int accountId, Permission perm);
        bool IsUsernameExists(string username);
        bool CreateAccount(Account newAcc);
        int LogIn(string username, string password);
    }
}