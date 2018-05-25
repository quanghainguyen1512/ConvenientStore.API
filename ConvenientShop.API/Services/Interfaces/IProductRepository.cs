using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvenientShop.API.Entities;

namespace ConvenientShop.API.Services.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int productId, bool includeDetail);
        ProductDetail GetProductDetail(string barcode);
        (IEnumerable<ProductDetail> result, int totalCount) GetAllProductDetails(Helpers.ProductDetailsResourceParameters parameters);
        int GetNumbersOfPage(int size);
        bool ProductExists(int productId);
        bool ProductDetailExists(string barcode);
        bool ExportFromRepo(string barcode, int quantity);
    }
}