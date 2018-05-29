using System;

namespace ConvenientShop.API.Models
{
    public class ExportDto
    {
        public string BarCode { get; set; }
        public string Name { get; set; }
        public int ExportedQuantity { get; set; }
        public DateTime ExportedDatetime { get; set; }
    }
}