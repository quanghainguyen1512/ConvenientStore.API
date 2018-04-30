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
    public class ProductRepository : ConvenientStoreRepository, Interfaces.IProductRepository
    {
        public ProductRepository(IOptions<StoreConfig> config) : base(config) { }

        public Product GetProduct(int productId, bool includeDetail)
        {
            using(var conn = Connection)
            {
                conn.Open();

                var dict = new Dictionary<int, Product>();

                var sql = new StringBuilder("SELECT * FROM product as p ");
                sql.Append("INNER JOIN supplier as s ON p.SupId = s.SupplierId ");
                sql.Append("INNER JOIN category as c ON p.CateId = c.CategoryId ");
                if (includeDetail)
                {
                    sql.Append("INNER JOIN product_detail as pd ON p.ProductId = pd.ProId ");
                }
                sql.Append("WHERE p.ProductId = @productId");

                return includeDetail ?
                    conn.Query<Product, Supplier, Category, ProductDetail, Product>(
                        sql.ToString(),
                        map: (p, s, c, pd) =>
                        {
                            p.Supplier = s;
                            p.Category = c;

                            if (!dict.TryGetValue(p.ProductId, out var entry))
                            {
                                entry = p;
                                entry.Details = new List<ProductDetail>();
                                dict.Add(entry.ProductId, entry);
                            }
                            p.Details.Add(pd);
                            return p;
                        },
                        param : new { productId },
                        splitOn: "SupplierId, CategoryId, BarCode"
                    ).FirstOrDefault() :
                    conn.Query<Product, Supplier, Category>(
                        sql.ToString(),
                        splitOn: "SupplierId, CategoryId",
                        param : new { productId }
                    ).FirstOrDefault();
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using(var conn = Connection)
            {
                conn.Open();
                return conn.GetAll<Product>();
            }
        }
    }
}