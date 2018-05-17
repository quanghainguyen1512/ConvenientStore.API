using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace ConvenientShop.API.Services
{
    public class AccountRepository : ConvenientStoreRepository, IAccountRepository
    {
        public AccountRepository(IOptions<StoreConfig> config) : base(config) { }

        public bool AuthorizeUser(int accId, Permission perm)
        {
            using(var conn = Connection)
            {
                var sql = "SELECT Username FROM has_permission as hp " +
                    "INNER JOIN account as a " +
                    "ON a.RoleId = hp.RoleId " +
                    "WHERE hp.PermissionId = @permId AND a.AccountId = @accId";
                var res = conn.ExecuteScalar(sql, param : new { permId = (int) perm, accId });
                return res != null;
            }
        }

        public bool CreateAccount(Account newAcc)
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.Insert(newAcc) != 0;
            }
        }

        public bool IsUsernameExists(string username)
        {
            using(var conn = Connection)
            {
                var sql = "SELECT Username FROM account WHERE Username like @username";
                var result = conn.ExecuteScalar(sql, param : new { username });
                return result != null;
            }
        }

        public int LogIn(string username, string password)
        {
            using(var conn = Connection)
            {
                var sql = "SELECT AccountId, Username FROM account WHERE Username like @username && Password like @password";
                var account = conn.QueryFirstOrDefault<AccountDto>(sql, param : new { username, password });
                if (account is AccountDto acc)
                    return acc.AccountId;
                return -1;
            }
        }
    }

    public enum Permission
    {
        ViewOneStaffInfo = 1,
        EditStaffInfo,
        OrderNewProduct,
        EditInventoryDetail,
        EditShelfDetail,
        ViewInventoryDetail,
        ViewShelfDetail,
        EditProductCategory,
        EditOffer,
        ViewOffer,
        ViewCustomerInfo,
        ViewBillHistory,
        ViewPermission,
        EditPermission,
        ViewAllStaffInfo,
        EditAccount,
        ViewAccount,
        ChangeMyPassword,
        EditCustomerInfo,
        AddBill
    }
}