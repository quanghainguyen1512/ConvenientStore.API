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

        [Write(false)]
        public int Price { get; set; }

        [Write(false)]
        public int QuantityInRepository { get; set; }

        [Write(false)]
        public DateTime ExpirationDate { get; set; }

        [Write(false)]
        public Product Product { get; set; }
    }
}