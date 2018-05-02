using System;

namespace ConvenientShop.API.Models
{
    public class ProductDetailForOperationsDto
    {
        public string BarCode { get; set; }
        public int QuantityOnStore { get; set; }
        public int QuantityInRepository { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}