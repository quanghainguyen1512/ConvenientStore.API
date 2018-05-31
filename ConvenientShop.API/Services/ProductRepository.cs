using System.Collections.Generic;
using System.Data;
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

        public bool ExportFromRepo(string barcode, int quantity)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "EXECUTE dbo.USP_ExportFromRepo @quantity, @barcode";
                return conn.Execute(
                    sql,
                    param : new { barcode, quantity }
                ) != 0;
            }
        }

        public(IEnumerable<ProductDetail> result, int totalCount) GetAllProductDetails(Helpers.ProductDetailsResourceParameters parameters)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var param = new DynamicParameters();
                param.Add("page", parameters.PageNumber);
                param.Add("size", parameters.PageSize);
                param.Add("searchQuery", parameters.SearchQuery);
                param.Add("totalCount", dbType : DbType.Int32, direction : ParameterDirection.Output);

                // var sql = "EXECUTE dbo.USP_GetProducDetailsWithPagination @page, @size, @totalCount = @count OUTPUT";
                var sql = "USP_GetProducDetailsWithPagination";
                var result = conn.Query<ProductDetail, Product>(
                        sql,
                        splitOn: "Name",
                        param : param,
                        commandType : CommandType.StoredProcedure
                    );
                var totalCount = param.Get<int>("totalCount");
                return (result, totalCount);
            }
        }

        public int GetNumbersOfPage(int size)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "EXECUTE dbo.USP_GetTotalPage @size";
                return conn.ExecuteScalar(
                    sql,
                    param : new { size }
                ) is int a ? a : -1;
            }
        }

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

        public ProductDetail GetProductDetail(string barcode)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "USP_GetOneProductDetail";
                var dict = new Dictionary<string, ProductDetail>();

                return conn.Query<ProductDetail, Product, Supplier, Category, Export, ProductDetail>(
                    sql,
                    map: (pd, p, s, c, e) =>
                    {
                        pd.Supplier = s;
                        pd.Product = p;
                        pd.Category = c;

                        if (!dict.TryGetValue(pd.BarCode, out var entry))
                        {
                            entry = pd;
                            entry.ExportHistory = new List<Export>();
                            dict.Add(entry.BarCode, entry);
                        }
                        entry.ExportHistory.Add(e);

                        return entry;
                    },
                    splitOn: "ProductId, SupplierId, CategoryId, ExportedDateTime",
                    param : new { barcode },
                    commandType : CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT p.ProductId, p.Name, p.ImageUrl, p.Unit, " +
                    "SUM(pd.QuantityOnStore) AS OnStore, " +
                    "SUM(pd.QuantityInRepository) AS InRepo, " + 
                    "s.SupplierId, s.SupplierName " +
                    "FROM product AS p " +
                    "INNER JOIN supplier AS s ON p.SupId = s.SupplierId " +
                    "INNER JOIN order_detail AS od ON od.ProductId = p.ProductId " +
                    "INNER JOIN shipment AS sh ON sh.OrderDetailId = od.OrderDetailId " +
                    "INNER JOIN product_detail AS pd ON pd.BarCode = sh.BarCode " +
                    "GROUP BY p.ProductId, p.Name, p.ImageUrl, p.Unit, " +
                    "s.SupplierId, s.SupplierName";
                return conn.Query<Product, Supplier>(
                    sql,
                    splitOn: "SupplierId"
                );
            }
        }

        public bool ProductDetailExists(string barcode)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT BarCode FROM product_detail " +
                    "WHERE BarCode = @barcode";
                return conn.ExecuteScalar(sql, param : new { barcode }) != null;
            }
        }

        public bool ProductExists(int productId)
        {
            using(var conn = Connection)
            {
                conn.Open();
                var sql = "SELECT ProductId FROM product WHERE ProductId = @productId";
                return conn.ExecuteScalar(sql, param : new { productId }) != null;
            }
        }
    }
}