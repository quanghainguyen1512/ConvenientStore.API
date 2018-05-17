using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }

        [Write(false)]
        public Supplier Supplier { get; set; }

        [Write(false)]
        public Category Category { get; set; }

        [Write(false)]
        public ICollection<ProductDetail> Details { get; set; }
    }
}