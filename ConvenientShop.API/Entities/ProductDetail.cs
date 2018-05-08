using System;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("product_detail")]
    public class ProductDetail
    {
        [ExplicitKey]
        public string BarCode { get; set; }
        public int Price { get; set; }
        public int QuantityOnStore { get; set; }
        public int QuantityInRepository { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProductId { get; set; }
        public int ShipmentId { get; set; }

        [Write(false)]
        public Product Product { get; set; }

        [Write(false)]
        public Shipment Shipment { get; set; }
    }
}