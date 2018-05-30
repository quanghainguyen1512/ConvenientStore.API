using System;
using System.Collections.Generic;
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

        [Write(false)]
        public Supplier Supplier { get; set; }

        [Write(false)]
        public Category Category { get; set; }

        [Write(false)]
        public ICollection<Export> ExportHistory { get; set; }
    }
}