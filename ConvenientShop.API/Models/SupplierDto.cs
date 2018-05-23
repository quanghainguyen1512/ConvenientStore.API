using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConvenientShop.API.Models
{
    public class SupplierDto : SupplierWithoutProductsDto
    {
        public ICollection<ProductWithoutDetailDto> Products { get; set; } = new List<ProductWithoutDetailDto>();
        public int NumberOfProducts => Products.Count;
    }
}