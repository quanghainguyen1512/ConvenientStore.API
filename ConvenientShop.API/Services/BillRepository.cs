using System.Collections.Generic;
using System.Linq;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;
using Microsoft.Extensions.Options;
using Z.Dapper.Plus;

namespace ConvenientShop.API.Services
{
    public class BillRepository : ConvenientStoreRepository, Interfaces.IBillRepository
    {
        public BillRepository(IOptions<StoreConfig> config) : base(config) { }

        public bool AddBill(Bill bill)
        {
            using(var conn = Connection)
            {
                conn.Open();
                DapperPlusManager.Entity<Bill>()
                    .Table("bill")
                    .Identity(b => b.BillId)
                    .Ignore(b => b.BillDetails)
                    .Ignore(b => b.Staff);
                DapperPlusManager.Entity<BillDetail>()
                    .Table("bill_detail")
                    .Ignore(bd => bd.Bill)
                    .Ignore(bd => bd.ProductDetail)
                    .Map(bd => new
                    {
                        bd.BillDetailId,
                            BillId = bd.Bill.BillId,
                            bd.Quantity,
                            bd.BarCode
                    });
                using(var tran = conn.BeginTransaction())
                {
                    var r = conn.BulkInsert(bill)
                        .ThenForEach(b => b.BillDetails.ForEach(bd => bd.Bill = b))
                        .ThenBulkInsert(b => b.BillDetails);
                    tran.Commit();
                    return true;
                }
            }
        }

        public Bill GetBill(int billId, bool includeDetail)
        {
            using(var conn = Connection)
            {
                conn.Open();
                if (!includeDetail)
                    return conn.Get<Bill>(billId);

                var sql = "SELECT b.BillId, b.CreatedDateTime, " +
                    // "c.CustomerId, c.FirstName, c.LastName, " +
                    "s.StaffId, s.FirstName, s.LastName, " +
                    "pd.BarCode, pd.Price, " +
                    "p.Name, " +
                    "bd.Quantity " +
                    "FROM bill as b " +
                    "INNER JOIN staff as s ON b.StaffId = s.StaffId " +
                    // "INNER JOIN customer as c ON b.CustomerId = c.CustomerId " +
                    "INNER JOIN bill_detail as bd ON b.BillId = bd.BillId " +
                    "INNER JOIN product_detail as pd ON bd.BarCode = pd.BarCode " +
                    "INNER JOIN product as p ON pd.ProId = p.ProductId " +
                    "WHERE b.BillId = @billId";

                var dict = new Dictionary<int, Bill>();

                return conn.Query<Bill, Staff, ProductDetail, Product, BillDetail, Bill>(
                    sql,
                    map: (b, s, pd, p, bd) =>
                    {
                        b.Staff = s;
                        pd.Product = p;
                        bd.ProductDetail = pd;

                        if (!dict.TryGetValue(b.BillId, out var entry))
                        {
                            entry = b;
                            entry.BillDetails = new List<BillDetail>();
                            dict.Add(entry.BillId, entry);
                        }
                        entry.BillDetails.Add(bd);
                        return entry;
                    },
                    splitOn: "StaffId, BarCode, Name, Quantity",
                    param : new { billId }
                ).FirstOrDefault();
            }
        }

        public IEnumerable<Bill> GetBills()
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.GetAll<Bill>();
            }
        }

        public IEnumerable<Bill> GetBillsByStaff(int staffId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT b.BillId, b.CreatedDateTime, s.StaffId, s.FirstName, s.LastName " +
                    "FROM bill as b " +
                    "INNER JOIN staff as s ON b.StaffId = s.StaffId " +
                    // "INNER JOIN customer as c ON b.CustomerId = c.CustomerId " +
                    "WHERE b.StaffId = @staffId";

                return conn.Query<Bill, Staff>(
                    sql,
                    splitOn: "StaffId",
                    param : new { staffId }
                );
            }
        }
    }
}