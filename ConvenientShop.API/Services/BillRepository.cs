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
                using(var tran = conn.BeginTransaction())
                {
                    var r = conn.BulkInsert(bill)
                        .ThenForEach(b =>
                        {
                            var list = b.BillDetails.ToList();
                            list.ForEach(bd => bd.Bill.BillId = b.BillId);
                        })
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

                var sql = "SELECT b.CreatedDateTime, b.TotalPrice, " +
                    "c.CustomerId, c.FirstName, c.LastName, " +
                    "s.StaffId, s.FirstName, s.LastName, " +
                    "pd.BarCode, " +
                    "p.Name, p.Price, " +
                    "bd.Quantity " +
                    "FROM bill as b " +
                    "INNER JOIN staff as s ON b.StaffId = s.StaffId " +
                    "INNER JOIN customer as c ON b.CustomerId = c.CustomerId " +
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
                    splitOn: "CustomerId, StaffId, BarCode, Name, Quantity",
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
                var sql = "SELECT b.CreatedDateTime, b.TotalPrice, s.StaffId, s.FirstName, s.LastName " +
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