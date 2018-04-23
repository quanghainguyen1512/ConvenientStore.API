using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }
        public string SupplierName { get; set; }
        public string Category { get; set; }
    }
}
