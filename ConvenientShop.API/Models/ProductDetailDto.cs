using System;

namespace ConvenientShop.API.Models
{
    public class ProductDetailDto
    {
        public string BarCode { get; set; }
        public int QuantityOnStore { get; set; }
        public int QuantityInRepository { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
    }
}