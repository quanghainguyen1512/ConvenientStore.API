using System;

namespace ConvenientShop.API.Entities
{
    public class Export
    {
        public string BarCode { get; set; }
        public int ExportedQuantity { get; set; }
        public DateTime ExportedDateTime { get; set; }
    }
}