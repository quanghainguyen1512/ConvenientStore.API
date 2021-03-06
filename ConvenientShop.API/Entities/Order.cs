using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("order_action")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int StaffId { get; set; }
        public int SupplierId { get; set; }
        
        [Write(false)]
        public Supplier Supplier { get; set; }
        [Write(false)]
        public Staff Staff { get; set; }

        [Write(false)]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}