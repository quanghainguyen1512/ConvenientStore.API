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

        [Write(false)]
        public Staff Staff { get; set; }

        [Write(false)]
        public ICollection<OrderDetail> Details { get; set; }
    }
}