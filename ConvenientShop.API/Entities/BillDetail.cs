using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("bill_detail")]
    public class BillDetail
    {
        [Key]
        public int BillDetailId { get; set; }
        public int Quantity { get; set; }
        public string BarCode { get; set; }
        // public int BillId { get; set; }

        [Write(false)]
        public ProductDetail ProductDetail { get; set; }

        [Write(false)]
        public Bill Bill { get; set; }
    }
}