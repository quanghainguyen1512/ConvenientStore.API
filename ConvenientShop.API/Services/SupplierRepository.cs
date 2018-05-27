using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;
using Microsoft.Extensions.Options;

namespace ConvenientShop.API.Services
{
    public class SupplierRepository : ConvenientStoreRepository, Interfaces.ISupplierRepository
    {
        public SupplierRepository(IOptions<StoreConfig> config) : base(config) { }

        public bool AddProductToSupplier(int supplierId, Product p)
        {
            using(var conn = Connection)
            {
                conn.Open();
                p.Supplier.SupplierId = supplierId;
                return conn.Insert(p) != 0;
            }
        }

        public bool AddSupplier(Supplier supplier)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var res = conn.Insert<Supplier>(supplier);
                return res != 0;
            }
        }

        public bool DeleteProductFromSupplier(int supplierId, Product proToDel)
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.Delete(proToDel);
            }
        }

        public Supplier GetSupplier(int supplierId, bool includeProducts)
        {

            using(var conn = Connection)
            {
                conn.Open();
                var supDictionaty = new Dictionary<int, Supplier>();

                var sql = new StringBuilder("SELECT * FROM supplier as s ");
                if (includeProducts)
                {
                    sql.Append("INNER JOIN product as p ON p.SupId = s.SupplierId ");
                }
                sql.Append("WHERE s.SupplierId = @supplierId");
                return includeProducts ?
                    conn.Query<Supplier, Product, Supplier>(
                        sql.ToString(),
                        (s, p) =>
                        {
                            if (!supDictionaty.TryGetValue(s.SupplierId, out var supEntry))
                            {
                                supEntry = s;
                                supEntry.Products = new List<Product>();
                                supDictionaty.Add(supEntry.SupplierId, supEntry);
                            }

                            supEntry.Products.Add(p);
                            return supEntry;
                        },
                        splitOn: "SupId",
                        param : new { supplierId }
                    ).FirstOrDefault() :
                    conn.Get<Supplier>(supplierId);
            }
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.GetAll<Supplier>();
            }
        }

        public bool SupplierExists(int supplierId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT SupplierId FROM supplier " +
                    "WHERE SupplierId = @supplierId";
                return conn.ExecuteScalar(sql, param : new { supplierId }) != null;
            }
        }

        public bool UpdateSupplier(Supplier supToUpdate)
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.Update(supToUpdate);
            }
        }

        public IEnumerable<Delivery> GetAllDeliveryForSupplier(int supplierId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT * FROM delivery WHERE SupplierId = @supplierId";
                return conn.Query<Delivery>(
                    sql,
                    param : new { supplierId }
                );
            }
        }

        public IEnumerable<Order> GetOrdersForSupplier(int supplierId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT * FROM order_action WHERE SupplierId = @supplierId";
                return conn.Query<Order, Supplier>(
                    sql,
                    param : new { supplierId }
                );
            }
        }
    }
}