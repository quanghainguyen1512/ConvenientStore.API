using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;
using Microsoft.Extensions.Options;
using Z.Dapper.Plus;

namespace ConvenientShop.API.Services
{
    public class OrderRepository : ConvenientStoreRepository, Interfaces.IOrderRepository
    {
        public OrderRepository(IOptions<StoreConfig> config) : base(config) { }

        public bool AddOrder(Order orderToAdd)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var tran = conn.BeginTransaction())
                {
                    DapperPlusManager.Entity<Order>().Table("order_action")
                        .Identity(o => o.OrderId)
                        .Ignore(o => o.Staff)
                        .Ignore(o => o.OrderDetails);
                    DapperPlusManager.Entity<OrderDetail>()
                        .Table("order_detail")
                        .Identity(od => od.OrderDetailId)
                        .Ignore(od => od.Product);
                    try
                    {
                        tran.BulkInsert(orderToAdd)
                            .ThenForEach(o => o.OrderDetails.ForEach(d => d.OrderId = o.OrderId))
                            .ThenBulkInsert(o => o.OrderDetails);
                    }
                    catch (System.Exception)
                    {
                        tran.Rollback();
                        return false;
                    }
                    tran.Commit();
                    return true;
                }
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT o.OrderId, o.OrderDateTime, s.FirstName, s.LastName FROM dbo.order_action AS o " +
                    "INNER JOIN dbo.staff AS s ON o.StaffId = s.StaffId";
                return conn.Query<Order, Staff>(sql, splitOn: "FirstName");
            }
        }

        public Order GetOrder(int orderId, bool includeDetails)
        {
            using(var conn = Connection)
            {
                conn.Open();

                if (!includeDetails)
                    return conn.Get<Order>(orderId);

                var sql = new StringBuilder("SELECT * FROM order_action AS o");
                sql.Append(" INNER JOIN dbo.staff AS s ON o.StaffId = s.StaffId");
                sql.Append(" INNER JOIN order_detail as od ON od.OrderId = o.OrderId");
                sql.Append(" WHERE o.OrderId = @orderId");

                var dict = new Dictionary<int, Order>();

                return conn.Query<Order, Staff, OrderDetail, Order>(
                    sql.ToString(),
                    map: (o, s, od) =>
                    {
                        o.Staff = s;
                        if (!dict.TryGetValue(o.OrderId, out var entry))
                        {
                            entry = o;
                            entry.OrderDetails = new List<OrderDetail>();
                            dict.Add(entry.OrderId, entry);
                        }

                        entry.OrderDetails.Add(od);
                        return entry;
                    },
                    splitOn: "StaffId, OrderDetailId",
                    param : new { orderId }
                ).FirstOrDefault();
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetailsForOrder(int orderId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT od.OrderDetailId, od.ProductQuantity, p.Name, od.IsReceived " +
                    "FROM order_detail AS od " +
                    "INNER JOIN product AS p ON od.ProductId = p.ProductId " +
                    "WHERE od.OrderId = @orderId";
                return conn.Query<OrderDetail, Product>(
                    sql,
                    splitOn: "Name",
                    param : new { orderId });
            }
        }

        public IEnumerable<Order> GetOrdersForStaff(int staffId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT o.OrderId, o.OrderDateTime FROM dbo.order_action AS o " +
                    "INNER JOIN dbo.staff AS s ON o.StaffId = s.StaffId " +
                    "WHERE o.StaffId = @staffId";
                return conn.Query<Order>(sql, param : new { staffId });
            }
        }

        public bool OrderDetailExists(int id, out int quantityinrepo)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var od = conn.Get<OrderDetail>(id);
                quantityinrepo = od.ProductQuantity;
                return od != null;
            }
        }
    }
}