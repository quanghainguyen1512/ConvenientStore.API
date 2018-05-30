using System;
using System.Collections;
using System.Collections.Generic;

namespace ConvenientShop.API.Models
{
    public class ProductDetailDto : ProductDetailSimpleDto
    {
        // public string BarCode { get; set; }
        // public int QuantityOnStore { get; set; }
        // public int QuantityInRepository { get; set; }
        // public string ProductName { get; set; }
        // public string Unit { get; set; }
        // public int Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SupplierName { get; set; }
        public string Category { get; set; }
        public IEnumerable<ExportForProductDetailDto> ExportHistory { get; set; }

    }
}