using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using ConvenientShop.API.Services.Interfaces;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace ConvenientShop.API.Services
{
    public class StaffRepository : ConvenientStoreRepository, IStaffRepository
    {
        public StaffRepository(IOptions<StoreConfig> config) : base(config)
        {

        }
        public bool AddStaff(Staff staff)
        {
            var rowsInfected = 0;
            using (var conn = Connection)
            {
                conn.Open();
                //var sql = "";
            }
            return rowsInfected != 0;
        }

        public bool DeleteStaff(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Staff> GetAllStaffs()
        {
            using (var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT s.FirstName, s.LastName, s.DateOfBirth, s.Gender, s.PhoneNumber, s.IdentityNumber FROM convenient_shop_db.staff as s";
                return conn.Query<Staff>(sql);
            }
        }

        public Staff GetStaff(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT s.FirstName, s.LastName, s.DateOfBirth, s.Gender, s.PhoneNumber, s.IdentityNumber FROM convenient_shop_db.staff as s" +
                    "WHERE StaffId = @id";
                return conn.Query<Staff>(sql, param: new { id }).FirstOrDefault();
            }
        }
    }
}
