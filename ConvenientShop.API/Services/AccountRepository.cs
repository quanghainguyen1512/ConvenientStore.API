using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services.Interfaces;
using Dapper;
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
                    "ON a.RoleId = hp.RoleId" +
                    "WHERE hp.PermissionId = @permId AND a.AccountId = @accId";
                var res = conn.ExecuteScalar(sql, param : new { permId = (int) perm, accId });
                return res != null;
            }
        }

        public void CreateAccount(string username, string password, int roleId)
        {
            using(var conn = Connection)
            {
                var sql = "INSERT INTO `convenient_shop_db`.`account` (`Username`, `Password`, `RoleId`) " +
                    "VALUES (@username, @password, @roleId)";
                conn.Execute(sql, param : new { username, password, roleId });
            }
        }

        public bool IsUsernameExists(string username)
        {
            using(var conn = Connection)
            {
                var sql = "SELECT Username FROM convenient_shop_db.account WHERE Username like @username";
                var result = conn.ExecuteScalar(sql, param : new { username });
                return result != null;
            }
        }

        public(bool, AccountDto) LogIn(string username, string password)
        {
            using(var conn = Connection)
            {
                var sql = "SELECT AccountId FROM convenient_shop_db.account WHERE Username like @username && Password like @password";
                var accountId = conn.QueryFirstOrDefault<AccountDto>(sql, param : new { username, password });
                return ((accountId is null), accountId as AccountDto);
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
        EditCustomerInfo
    }
}