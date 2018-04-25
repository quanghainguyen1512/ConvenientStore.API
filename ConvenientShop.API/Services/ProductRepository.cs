using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConvenientShop.API.Entities;
using ConvenientShop.API.Models;
using Dapper;
using Microsoft.Extensions.Options;

namespace ConvenientShop.API.Services
{
    public class ProductRepository : ConvenientStoreRepository, Interfaces.IProductRepository
    {
        public ProductRepository(IOptions<StoreConfig> config) : base(config) { }

        public Product GetProduct(int productId, bool includeDetail)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = new StringBuilder("SELECT * FROM product as p ");
                if (includeDetail)
                {
                    sql.Append("INNER JOIN supplier as s ON p.SupId = s.SupplierId ");
                    sql.Append("INNER JOIN category as c ON p.CateId = c.CategoryId ");
                }
                sql.Append("WHERE p.ProductId = @productId");

                return includeDetail ?
                    conn.Query<Product, Supplier, Category, Product>(
                        sql.ToString(),
                        map: (p, s, c) =>
                        {
                            p.Supplier = s;
                            p.Category = c;

                            return p;
                        },
                        param : new { productId },
                        splitOn: "SupplierId, CategoryId"
                    ).FirstOrDefault() :
                    conn.Query<Product>(sql.ToString(), param : new { productId }).FirstOrDefault();
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT p.Name, p.Price, p.Unit FROM product as p";
                return conn.Query<Product>(sql);
            }
        }
    }
}