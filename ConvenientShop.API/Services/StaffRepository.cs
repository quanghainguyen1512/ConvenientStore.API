using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Z.Dapper.Plus;

namespace ConvenientShop.API.Services
{
    public class StaffRepository : ConvenientStoreRepository, IStaffRepository
    {
        public StaffRepository(IOptions<StoreConfig> config) : base(config) { }
        public bool AddStaff(Staff staff, Account newAccount)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var tran = conn.BeginTransaction())
                {
                    DapperPlusManager.Entity<Account>()
                        .Table("account")
                        .Identity(a => a.AccountId);
                    DapperPlusManager.Entity<Staff>()
                        .Table("staff")
                        .Identity(s => s.StaffId)
                        .Ignore(s => s.Bills);
                    try
                    {
                        tran.BulkInsert(newAccount)
                            .ThenForEach(a => staff.AccountId = a.AccountId)
                            .BulkInsert(staff);
                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool DeleteStaff(Staff staffToDel)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var tran = conn.BeginTransaction())
                {
                    var accountId = staffToDel.AccountId;
                    staffToDel.AccountId = -1;
                    conn.Update<Staff>(staffToDel, tran);
                    conn.Delete<Account>(new Account { AccountId = accountId }, tran);
                    tran.Commit();
                    return true;
                }
            }
        }

        public IEnumerable<Staff> GetAllStaffs()
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT s.StaffId, s.DateOfBirth, s.FirstName, s.LastName, s.PhoneNumber, s.Gender, s.ImageUrl, s.IdentityNumber, " +
                    "r.RoleId, r.Name FROM staff AS s " +
                    "LEFT JOIN account AS a ON s.AccountId = a.AccountId " +
                    "INNER JOIN role AS r ON r.RoleId = a.RoleId " +
                    "WHERE s.AccountId <> -1";
                return conn.Query<Staff, Role>(sql, splitOn: "RoleId");
            }
        }

        public Staff GetStaff(int id)
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.Get<Staff>(id);
            }
        }

        public bool StaffExists(int id)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT Staffid FROM staff WHERE StaffId = @id";
                return conn.ExecuteScalar(sql, param : new { id }) != null;
            }
        }

        public bool UpdateStaff(Staff staffToUpdate)
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.Update(staffToUpdate);
            }
        }
    }
}