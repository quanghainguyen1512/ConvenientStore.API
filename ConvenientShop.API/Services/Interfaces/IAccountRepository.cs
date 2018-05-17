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
        bool IsAccountExists(string username);
        bool IsAccountExists(int accountId);
        bool CreateAccount(Account newAcc);
        bool DeleteAccount(int accountId);
        int LogIn(string username, string password);
    }
}