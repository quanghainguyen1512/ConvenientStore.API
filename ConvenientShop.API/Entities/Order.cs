using System;
using Dapper.Contrib.Extensions;

namespace ConvenientShop.API.Entities
{
    [Table("order_action")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public Staff Staff { get; set; }
    }
}