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
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Z.Dapper.Plus;

namespace ConvenientShop.API.Services
{
    public class StaffRepository : ConvenientStoreRepository, IStaffRepository
    {
        public StaffRepository(IOptions<StoreConfig> config) : base(config) { }
        public bool AddStaff(Staff staff)
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.Insert(staff) != 0;
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
                return conn.GetAll<Staff>();
            }
        }

        public Staff GetStaff(int id)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT s.FirstName, s.LastName, s.DateOfBirth, s.Gender, s.PhoneNumber, s.IdentityNumber FROM staff as s " +
                    "WHERE StaffId = @id";
                return conn.Query<Staff>(sql, param : new { id }).FirstOrDefault();
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