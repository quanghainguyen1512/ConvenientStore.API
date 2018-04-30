using System;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("product_detail")]
    public class ProductDetail
    {
        [ExplicitKey]
        public string BarCode { get; set; }
        public int QuantityOnStore { get; set; }
        public int QuantityInRepository { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Product Product { get; set; }
        // public Shipment Shipment { get; set; }
    }
}