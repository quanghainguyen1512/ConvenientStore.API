using System;
using System.Collections;

namespace ConvenientShop.API.Models
{
    public class ProductWithoutDetailDto
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string SupplierName { get; set; }
        public string ImageUrl { get; set; }
        public int OnStore { get; set; }
        public int InRepo { get; set; }
    }
}