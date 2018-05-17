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

        public bool DeleteAccount(int accountId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var tran = conn.BeginTransaction())
                {
                    var sql = "UPDATE staff SET AccountId = NULL WHERE AccountId = @accountId";
                    if (conn.Execute(sql, param : new { accountId }) == 0)
                        return false;
                    sql = "DELETE FROM account WHERE AccountId = @accountId";
                    if (conn.Execute(sql, param : new { accountId }) == 0)
                        return false;

                    tran.Commit();
                    return true;
                }
            }
        }

        public bool IsAccountExists(string username)
        {
            using(var conn = Connection)
            {
                var sql = "SELECT Username FROM account WHERE Username like @username";
                var result = conn.ExecuteScalar(sql, param : new { username });
                return result != null;
            }
        }

        public bool IsAccountExists(int accountId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var acc = conn.Get<Account>(accountId);
                return acc != null;
            }
        }

        public Staff LogIn(string username, string password)
        {
            using(var conn = Connection)
            {
                var sql = "SELECT s.AccountId, s.StaffId, s.FirstName, s.LastName FROM account AS a " +
                    "INNER JOIN staff AS s ON s.AccountId = a.AccountId " +
                    "WHERE Username like @username && Password like @password";
                return conn.QueryFirstOrDefault<Staff>(sql, param : new { username, password });
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
        ViewOrder,
        ViewCustomerInfo,
        ViewBillHistory,
        ViewPermission,
        EditPermission,
        ViewAllStaffInfo,
        EditAccount,
        ViewAccount,
        ChangeMyPassword,
        EditCustomerInfo,
        AddBill,
        DeleteAccount
    }
}