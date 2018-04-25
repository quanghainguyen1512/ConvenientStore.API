using System;

namespace ConvenientShop.API.Entities
{
    public class ProductDetail
    {
        public string BarCode { get; set; }
        public int QuantityOnStore { get; set; }
        public int QuantityInRepository { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Product Product { get; set; }
        // public Shipment Shipment { get; set; }
    }
}