using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using Dapper;
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
                var sql = "INSERT INTO `mrwhoami_convenient_store`.`product` (`SupId`, `CateId`, `Name`, `Price`, `Unit`)" +
                    $"VALUES ('{supplierId}', '{p.Category.CategoryId}', '{p.Name}', {p.Price}, '{p.Unit}')";
                return conn.Execute(sql) > 0;
            }
        }

        public bool AddSupplier(Supplier supplier)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "INSERT INTO `mrwhoami_convenient_store`.`supplier`(`SupplierName`, `Address`, `PhoneNumber`, `Email`)" +
                    $" VALUES('{supplier.SupplierName}', '{supplier.Address}', '{supplier.PhoneNumber}', '{supplier.Email}')";
                var res = conn.Execute(sql);
                return res > 0;
            }
        }

        public void DeleteProductFromSupplier(int supplierId, int productId)
        {
            throw new System.NotImplementedException();
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
                    conn.Query<Supplier>(sql.ToString(), param : new { supplierId }).FirstOrDefault();
            }
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT SupplierName, Address, PhoneNumber, Email FROM mrwhoami_convenient_store.supplier;";
                return conn.Query<Supplier>(sql);
            }
        }

        public bool SupplierExists(int supplierId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT SupplierId FROM mrwhoami_convenient_store.supplier" +
                    "WHERE SupplerId = @supplierId";
                return conn.ExecuteScalar(sql, param : new { supplierId }) != null;
            }
        }
    }
}