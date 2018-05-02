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
        public int Price { get; set; }
        public string Unit { get; set; }
        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductDetail> Details { get; set; }
    }
}